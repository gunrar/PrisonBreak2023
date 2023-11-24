using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public Module[] modules = new Module[3];

    public void EquipPrimaryWeapon(Weapon newWeapon)
    {
        primaryWeapon = newWeapon;
        // Handle the equipping logic, such as updating the UI or stats
    }

    public void EquipSecondaryWeapon(Weapon newWeapon)
    {
        secondaryWeapon = newWeapon;
        // Handle the equipping logic
    }

    public void EquipModule(int slot, Module newModule)
    {
        if (slot >= 0 && slot < modules.Length)
        {
            modules[slot] = newModule;
            // Handle the equipping logic
        }
        else
        {
            Debug.LogError("Invalid module slot.");
        }
    }

    // Call these equip functions when you want to change equipment
}
