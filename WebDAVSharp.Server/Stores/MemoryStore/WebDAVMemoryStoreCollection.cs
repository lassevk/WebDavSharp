using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WebDAVSharp.Server.Exceptions;

namespace WebDAVSharp.Server.Stores.MemoryStore
{
    /// <summary>
    /// This class implements a memory-based <see cref="IWebDAVStoreCollection"/>.
    /// </summary>
    [DebuggerDisplay("Collection ({Name})")]
    public class WebDAVMemoryStoreCollection : WebDAVMemoryStoreItem, IWebDAVStoreCollection
    {
        private readonly Dictionary<string, WebDAVMemoryStoreItem> _Items = new Dictionary<string, WebDAVMemoryStoreItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVMemoryStoreCollection"/> class.
        /// </summary>
        /// <param name="parentCollection">The parent <see cref="IWebDAVStoreCollection"/> that contains this <see cref="IWebDAVStoreItem"/> implementation.</param>
        /// <param name="name">The name of this <see cref="IWebDAVStoreItem"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
        public WebDAVMemoryStoreCollection(IWebDAVStoreCollection parentCollection, string name)
            : base(parentCollection, name)
        {
            // Do nothing here
        }

        #region IWebDAVStoreCollection Members

        /// <summary>
        /// Gets a collection of all the items in this <see cref="IWebDAVStoreCollection"/>.
        /// </summary>
        public IEnumerable<IWebDAVStoreItem> Items
        {
            get
            {
                return _Items.Values.ToArray();
            }
        }

        /// <summary>
        /// Retrieves a store item by its name.
        /// </summary>
        /// <param name="name">
        /// The name of the store item to retrieve.
        /// </param>
        /// <returns>
        /// The store item that has the specified <paramref name="name"/>;
        /// or <c>null</c> if there is no store item with that name.
        /// </returns>
        public IWebDAVStoreItem GetItemByName(string name)
        {
            WebDAVMemoryStoreItem item;
            if (_Items.TryGetValue(name, out item))
                return item;
            return null;
        }

        /// <summary>
        /// Creates a new collection with the specified name.
        /// </summary>
        /// <param name="name">
        /// The name of the new collection.
        /// </param>
        /// <returns>
        /// The created <see cref="IWebDAVStoreCollection"/> instance.
        /// </returns>
        public IWebDAVStoreCollection CreateCollection(string name)
        {
            if (GetItemByName(name) != null)
                throw new HttpConflictException();

            var collection = new WebDAVMemoryStoreCollection(this, name);
            _Items[name] = collection;
            return collection;
        }

        /// <summary>
        /// Deletes a store item by its name.
        /// </summary>
        /// <param name="item">
        /// The name of the store item to delete.
        /// </param>
        public void Delete(IWebDAVStoreItem item)
        {
            _Items.Remove(item.Name);
        }

        /// <summary>
        /// Creates a new document with the specified name.
        /// </summary>
        /// <param name="name">
        /// The name of the new document.
        /// </param>
        /// <returns>
        /// The created <see cref="IWebDAVStoreDocument"/> instance.
        /// </returns>
        public IWebDAVStoreDocument CreateDocument(string name)
        {
            if (GetItemByName(name) != null)
                throw new HttpConflictException();

            var document = new WebDAVMemoryStoreDocument(this, name);
            _Items[name] = document;
            return document;
        }

        /// <summary>
        /// Copies an existing store item into this collection, overwriting any existing items.
        /// </summary>
        /// <param name="source">
        /// The store item to copy from.
        /// </param>
        /// <param name="destinationName">
        /// The name of the copy to create of <paramref name="source"/>.
        /// </param>
        /// <returns>
        /// The created <see cref="IWebDAVStoreItem"/> instance.
        /// </returns>
        public IWebDAVStoreItem CopyItemHere(IWebDAVStoreItem source, string destinationName)
        {
            if (source is IWebDAVStoreDocument)
                return CopyDocument(source as IWebDAVStoreDocument, destinationName);
            else if (source is IWebDAVStoreCollection)
                return CopyCollection(source as IWebDAVStoreCollection, destinationName);
            else
                throw new HttpInternalServerException();
        }

        /// <summary>
        /// Moves an existing store item into this collection, overwriting any existing items.
        /// </summary>
        /// <param name="source">
        /// The store item to move.
        /// </param>
        /// <param name="destinationName">
        /// The <see cref="IWebDAVStoreItem"/> that refers to the item that was moved,
        /// in its new location.
        /// </param>
        /// <returns>
        /// The moved <see cref="IWebDAVStoreItem"/> instance.
        /// </returns>
        /// <remarks>
        /// Note that the method should fail without creating or overwriting content in the
        /// target collection if the move cannot go through.
        /// </remarks>
        public IWebDAVStoreItem MoveItemHere(IWebDAVStoreItem source, string destinationName)
        {
            IWebDAVStoreItem item = CopyItemHere(source, destinationName);
            source.ParentCollection.Delete(source);
            return item;
        }

        private IWebDAVStoreDocument CopyDocument(IWebDAVStoreDocument source, string destinationName)
        {
            IWebDAVStoreItem existingItem = GetItemByName(destinationName);
            if (existingItem != null)
                Delete(existingItem);

            IWebDAVStoreDocument destination = CreateDocument(destinationName);
            using (Stream sourceStream = source.OpenReadStream())
            using (Stream destinationStream = destination.OpenWriteStream(false))
            {
                sourceStream.CopyTo(destinationStream);
            }

            return destination;
        }

        private IWebDAVStoreCollection CopyCollection(IWebDAVStoreCollection source, string destinationName)
        {
            IWebDAVStoreItem existingItem = GetItemByName(destinationName);
            if (existingItem != null)
                Delete(existingItem);

            IWebDAVStoreCollection destination = CreateCollection(destinationName);
            foreach (IWebDAVStoreItem subItem in source.Items)
                destination.CopyItemHere(subItem, subItem.Name);

            return destination;
        }

        #endregion
    }
}