using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitMovement movement;
    [SerializeField] private UnitTargeting targeting;

    private const float instantKillDamage = 999999999f;

    private UnitNodesHolder nodesHolder = new UnitNodesHolder();
    private UnitParameters parameters;

    public UnitMovement Movement => movement;
    public UnitTargeting Targeting => targeting;
    public UnitParameters Parameters => parameters;
    public UnitNodesHolder NodesHolder => nodesHolder;

    public virtual void Release(UnitParameters parameters)
    {
        this.parameters = parameters;
        parameters.OnDie += Die;

        gameObject.SetActive(true);
    }

    public void Kill()
    {
        parameters.ReduceHealth(instantKillDamage);
    }

    public void Hit(float value)
    {
        parameters.ReduceHealth(value);
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
        ReturnToManager();
    }

    private void ReturnToManager()
    {
        UnitsSpawner.Instance.AddInactiveUnit(this);
    }



    public void Destroy()
    {
        parameters.OnDie -= Die;

        Kill();
        Destroy(gameObject);
    }



    public class UnitParameters
    {
        private float startHealth;
        private float health;
        private bool isAlive;

        public float StartHealth => startHealth;
        public float Health => health;
        public bool IsAlive => isAlive;

        public System.Action OnHealthChanged = () => { };
        public System.Action OnDie = () => { };

        public UnitParameters(float startHealth)
        {
            this.startHealth = startHealth;
            this.health = startHealth;
            this.isAlive = true;
        }

        public void ReduceHealth(float value)
        {
            health -= value;

            OnHealthChanged.Invoke();

            if (health <= 0 && isAlive)
            {
                health = 0;
                isAlive = false;

                OnDie.Invoke();
            }
        }
    }
}
