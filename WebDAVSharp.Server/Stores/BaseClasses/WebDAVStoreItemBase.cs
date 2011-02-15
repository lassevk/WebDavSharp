using System;
using WebDAVSharp.Server.Exceptions;

namespace WebDAVSharp.Server.Stores.BaseClasses
{
    /// <summary>
    /// This is the base class for <see cref="IWebDAVStoreItem"/> implementations.
    /// </summary>
    public class WebDAVStoreItemBase : IWebDAVStoreItem
    {
        private readonly IWebDAVStoreCollection _ParentCollection;
        private string _Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVStoreItemBase"/> class.
        /// </summary>
        /// <param name="parentCollection">
        /// The parent <see cref="IWebDAVStoreCollection"/> that contains this <see cref="IWebDAVStoreItem"/> implementation.
        /// </param>
        /// <param name="name">
        /// The name of this <see cref="IWebDAVStoreItem"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="name"/> is <c>null</c>.</para>
        /// </exception>
        protected WebDAVStoreItemBase(IWebDAVStoreCollection parentCollection, string name)
        {
            if (StringEx.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            _ParentCollection = parentCollection;
            _Name = name;
        }

        #region IWebDAVStoreItem Members

        /// <summary>
        /// Gets the parent <see cref="IWebDAVStoreCollection"/> that owns this <see cref="IWebDAVStoreItem"/>.
        /// </summary>
        public IWebDAVStoreCollection ParentCollection
        {
            get
            {
                return _ParentCollection;
            }
        }

        /// <summary>
        /// Gets or sets the name of this <see cref="IWebDAVStoreItem"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                string fixedName = (value ?? string.Empty).Trim();
                if (fixedName != _Name)
                {
                    if (!OnNameChanging(_Name, fixedName))
                        throw new HttpException(HttpStatusCodes.ClientErrors.Forbidden);
                    string oldName = _Name;
                    _Name = fixedName;
                    OnNameChanged(oldName, _Name);
                }
            }
        }

        #endregion

        /// <summary>
        /// Called before the name of this <see cref="IWebDAVStoreItem"/> is changing.
        /// </summary>
        /// <param name="oldName">
        /// The old name of this <see cref="IWebDAVStoreItem"/>.
        /// </param>
        /// <param name="newName">
        /// The new name of this <see cref="IWebDAVStoreItem"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the name change is allowed;
        /// otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool OnNameChanging(string oldName, string newName)
        {
            return true;
        }

        /// <summary>
        /// Called after the name of this <see cref="IWebDAVStoreItem"/> has changed.
        /// </summary>
        /// <param name="oldName">
        /// The old name of this <see cref="IWebDAVStoreItem"/>.
        /// </param>
        /// <param name="newName">
        /// The new name of this <see cref="IWebDAVStoreItem"/>.
        /// </param>
        protected virtual void OnNameChanged(string oldName, string newName)
        {
        }
    }
}