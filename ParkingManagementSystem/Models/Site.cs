using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Models
{
    internal class Site
    {
        public Site(Guid id, string name, string lat, string @long)
        {
            Id = id;
            Name = name;
            Lat = lat;
            Long = @long;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Lat { get; private set; }
        public string Long { get; private set; }
    }
}
