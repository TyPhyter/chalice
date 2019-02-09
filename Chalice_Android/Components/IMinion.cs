

namespace Chalice_Android.Components
{
    public interface IMinion
    {
        AttackValues _AtkVals { get; set; }
        int _Health { get; set; }
    };

    public struct AttackValues
    {
        int N;
        int E;
        int S;
        int W;

        public AttackValues(int n, int e, int s, int w)
        {
            N = n;
            E = e;
            S = s;
            W = w;
        }
    }
}