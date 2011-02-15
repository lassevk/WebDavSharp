using System;

namespace WebDAVSharp
{
    /// <summary>
    /// This class holds a single HTTP status code containing a unique id and a name.
    /// </summary>
    public class StatusCode
    {
        private readonly int _Id;
        private readonly string _Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCode"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of this <see cref="StatusCode"/>.
        /// </param>
        /// <param name="name">
        /// The name belonging to the <paramref name="id"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="name"/> is <c>null</c>.</para>
        /// </exception>
        public StatusCode(int id, string name)
        {
            _Id = id;
            _Name = name;
            if (StringEx.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
        }

        /// <summary>
        /// Gets the name of this <see cref="StatusCode"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// Gets the id of this <see cref="StatusCode"/>.
        /// </summary>
        public int Id
        {
            get
            {
                return _Id;
            }
        }
    }
}