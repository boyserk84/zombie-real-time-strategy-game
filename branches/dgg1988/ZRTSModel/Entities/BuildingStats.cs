using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Entities
{
    public class BuildingStats
    {
        public string buildingType;

        // Dimensions of the building
        public byte width = 1;
        public byte height = 1;

        // Max Health of the building
        public short maxHealth = 2000;

        // Can resources be dropped off at this building?
        public bool dropOffResources = false;

        public override string ToString()
        {
            string output = "Building Stats:\n";
            output += "Type:\t\t\t" + buildingType + "\n";
            output += "Dimensions:\t\t" + width + " X " + height + "\n";
            output += "Max Health:\t\t" + maxHealth + "\n";
            output += "Drop off Resources:\t" + dropOffResources + "\n";

            return output;
        }
    }
}
