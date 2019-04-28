public interface Attackable
{
    void GiveDamage(Attackable target, float damage);
    void TakeDamage(float damage);
}
