# C-Sharp 


public Monster ShallowClone()
{
     Monster copy = new Monster(Health, Mana, Ammo, Name);
     //copy.Health = this.Health;
     //copy.Name = this.Name;
     //copy.Ammo = this.Ammo;
     //copy.Mana = this.Mana;
     return copy;
     // return (Monster)this.MemberwiseClone();
}
