using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CloudyBoxLib.Model
{
    /// <summary>
    /// File or folders metadat
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{Path} IsDir = {IsDirectory}")]
    public class Metadata
    {
        /// <summary>
        /// Gets or sets the size of file of folder
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [DataMember(Name = "size")]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the bytes.
        /// </summary>
        /// <value>
        /// The bytes.
        /// </value>
        [DataMember(Name = "bytes")]
        public int Bytes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether thumbnail exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if thumbnail exists; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "thumb_exists")]
        public bool ThumbnailExists { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        /// <value>
        /// The rev.
        /// </value>
        [DataMember(Name = "rev")]
        public string Rev { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        [DataMember(Name = "modified")]
        public string Modified { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is directory.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is directory; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "is_dir")]
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        /// <value>
        /// The root.
        /// </value>
        [DataMember(Name = "root")]
        public string Root { get; set; }

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        [DataMember(Name = "contents")]
        public List<Metadata> Contents { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        [DataMember(Name = "revision")]
        public int Revision { get; set; }
    }
}