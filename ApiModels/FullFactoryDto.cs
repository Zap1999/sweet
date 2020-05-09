using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class FullFactoryDto
    {
        public Factory Factory { get; set; } // factoryUnits <- category, controller user
        public IEnumerable<UnitWorker> UnitWorkers { get; set; }
    }
}