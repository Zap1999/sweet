using System.Collections;
using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class UserExpanseDataDto
    {
        public IEnumerable<User> Users { get; set; }

        public Dictionary<string, int> Expanses { get; set; }
    }
}