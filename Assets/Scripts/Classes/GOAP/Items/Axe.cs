using Behaviours.GOAP;
using Interfaces;

namespace Classes.Items.GOAP
{
    public class Axe : ItemBase, ICreatable
    {
        public int BuildPoints { get; set; } = 100;
    }
}