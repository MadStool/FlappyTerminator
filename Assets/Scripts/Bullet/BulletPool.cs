using UnityEngine;

public class BulletPool : Pool<Bullet>, IBulletProvider
{
    protected override Bullet CreateItem()
    {
        Bullet bullet = base.CreateItem();
        bullet.Initialize(this);

        return bullet;
    }

    public Bullet GetBullet() => GetItem();
    public void ReturnBullet(Bullet bullet) => ReturnItem(bullet);
}