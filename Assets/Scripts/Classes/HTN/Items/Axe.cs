using Behaviours.HTN;
using Interfaces;

namespace Classes.Items.HTN
{
    public class Axe : ItemBase, ICreatable
    {
        public int BuildPoints { get; set; } = 100;
    }
}