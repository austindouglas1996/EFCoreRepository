using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    /// <summary>
    /// Defines an entity item when being used in an database. TKey represents the primary key type between int,string,guid.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
