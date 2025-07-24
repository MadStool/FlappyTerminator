using UnityEngine;

public interface IBulletProvider
{
    Bullet GetBullet();
    void ReturnBullet(Bullet bullet);
}
