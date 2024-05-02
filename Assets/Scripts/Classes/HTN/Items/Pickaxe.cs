using Behaviours.HTN;
using Interfaces;

namespace Classes.Items.HTN
{
    public class Pickaxe : ItemBase, ICreatable
    {
        public int BuildPoints { get; set; } = 200;
    }
}