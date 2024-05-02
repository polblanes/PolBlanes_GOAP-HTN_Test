using Behaviours.GOAP;
using Interfaces;

namespace Classes.Items.GOAP
{
    public class Pickaxe : ItemBase, ICreatable
    {
        public int BuildPoints { get; set; } = 200;
    }
}