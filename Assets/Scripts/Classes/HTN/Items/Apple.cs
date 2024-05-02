using Behaviours.HTN;
using Interfaces;

namespace Classes.Items.HTN
{
    public class Apple : ItemBase, IEatable
    {
        public float NutritionValue { get; set; } = 200f;

        public override bool ShouldDestroy()
        {
            if (base.ShouldDestroy())
                return true;

            if (NutritionValue <= 0f)
                return true;

            return false;
        }
    }
}