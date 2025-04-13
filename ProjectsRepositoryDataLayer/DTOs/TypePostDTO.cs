using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for TypePost
    /// </summary>
    public class TypePostDTO
    {
        public int TypePostID { get; set; }
        public string TypePostName { get; set; }

        public TypePostDTO(int typePostID, string typePostName)
        {
            TypePostID = typePostID;
            TypePostName = typePostName;
        }
    }
}
