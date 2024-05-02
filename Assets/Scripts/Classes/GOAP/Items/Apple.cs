using Behaviours.GOAP;
using Interfaces;

namespace Classes.Items.GOAP
{
    public class Apple : ItemBase, IEatable
    {
        public float NutritionValue { get; set; } = 200f;
    }
}