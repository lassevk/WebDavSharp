using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WebDAVSharp.Server.Exceptions;

namespace WebDAVSharp.Server.Stores.DiskStore
{
    /// <summary>
    /// This class implements a disk-based <see cref="IWebDAVStore"/> that maps to a folder on disk.
    /// </summary>
    [DebuggerDisplay("Directory ({Name})")]
    public sealed class WebDAVDiskStoreCollection : WebDAVDiskStoreItem, IWebDAVStoreCollection
    {
        private readonly Dictionary<string, WeakReference> _Items = new Dictionary<string, WeakReference>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVDiskStoreCollection"/> class.
        /// </summary>
        /// <param name="parentCollection">
        /// The parent <see cref="WebDAVDiskStoreCollection"/> that contains this <see cref="WebDAVDiskStoreCollection"/>.
        /// </param>
        /// <param name="path">
        /// The path to the folder on this that this <see cref="WebDAVDiskStoreCollection"/> maps to.
        /// </param>
        public WebDAVDiskStoreCollection(WebDAVDiskStoreCollection parentCollection, string path)
            : base(parentCollection, path)
        {
            // Nothing here
        }

        #region IWebDAVStoreCollection Members

        /// <summary>
        /// Gets a collection of all the items in this <see cref="IWebDAVStoreCollection"/>.
        /// </summary>
        public IEnumerable<IWebDAVStoreItem> Items
        {
            get
            {
                var toDelete = new HashSet<WeakReference>(_Items.Values);
                var items = new List<IWebDAVStoreItem>();

                // TODO: Refactor to get rid of duplicate loop code
                foreach (string subDirectoryPath in Directory.GetDirectories(ItemPath))
                {
                    string name = Path.GetFileName(subDirectoryPath);
                    WebDAVDiskStoreCollection collection = null;

                    WeakReference wr;
                    if (_Items.TryGetValue(name, out wr))
                    {
                        collection = wr.Target as WebDAVDiskStoreCollection;
                        if (collection == null)
                            toDelete.Remove(wr);
                    }

                    if (collection == null)
                    {
                        collection = new WebDAVDiskStoreCollection(this, subDirectoryPath);
                        _Items[name] = new WeakReference(collection);
                    }

                    items.Add(collection);
                }
                foreach (string filePath in Directory.GetFiles(ItemPath))
                {
                    string name = Path.GetFileName(filePath);
                    WebDAVDiskStoreDocument document = null;

                    WeakReference wr;
                    if (_Items.TryGetValue(name, out wr))
                    {
                        document = wr.Target as WebDAVDiskStoreDocument;
                        if (document == null)
                            toDelete.Remove(wr);
                    }

                    if (document == null)
                    {
                        document = new WebDAVDiskStoreDocument(this, filePath);
                        _Items[name] = new WeakReference(document);
                    }
                    items.Add(document);
                }

                return items.ToArray();
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
            string path = Path.Combine(ItemPath, name);
            if (File.Exists(path))
                return new WebDAVDiskStoreDocument(this, path);

            if (Directory.Exists(path))
                return new WebDAVDiskStoreCollection(this, path);

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
            string path = Path.Combine(ItemPath, name);
            Directory.CreateDirectory(path);
            return GetItemByName(name) as IWebDAVStoreCollection;
        }

        /// <summary>
        /// Deletes a store item by its name.
        /// </summary>
        /// <param name="item">
        /// The name of the store item to delete.
        /// </param>
        public void Delete(IWebDAVStoreItem item)
        {
            var diskItem = (WebDAVDiskStoreItem)item;
            string itemPath = diskItem.ItemPath;
            if (item is WebDAVDiskStoreDocument)
            {
                if (!File.Exists(itemPath))
                    throw new HttpNotFoundException();
                File.Delete(itemPath);
            }
            else
            {
                if (!Directory.Exists(itemPath))
                    throw new HttpNotFoundException();
                Directory.Delete(diskItem.ItemPath);
            }
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
            string itemPath = Path.Combine(ItemPath, name);
            if (File.Exists(itemPath) || Directory.Exists(itemPath))
                throw new HttpException(HttpStatusCodes.ClientErrors.Conflict);

            File.Create(ItemPath).Dispose();
            var document = new WebDAVDiskStoreDocument(this, itemPath);
            _Items.Add(name, new WeakReference(document));
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion
    }
}