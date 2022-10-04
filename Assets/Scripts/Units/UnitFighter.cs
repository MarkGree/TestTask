public class UnitFighter : Unit
{
    protected UnitParametersFighter fighterParameters;

    public float Damage => fighterParameters.Damage;

    public override void Release(UnitParameters parameters)
    {
        base.Release(parameters);

        fighterParameters = parameters as UnitParametersFighter;
    }


    public class UnitParametersFighter : UnitParameters
    {
        private float damage;
        public float Damage => damage;

        public UnitParametersFighter(float startHealth) : base(startHealth)
        {
        }
        public UnitParametersFighter(float startHealth, float damage) : base(startHealth)
        {
            this.damage = damage;
        }
    }
}
