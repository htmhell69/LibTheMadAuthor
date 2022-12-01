using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : Entity
{
    [SerializeField] KeyCode abilityKey;
    [SerializeField] float cooldown;
    [SerializeField] Vector3 handPositionOffset;
    [SerializeField] AudioClip specialAbilityNoise;
    Item activeItem;
    Slider hpSlider;
    Slider abilitySlider;
    Item collidedItem;
    UiHandler ui;
    GameManager gameManager;
    public override void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        base.Init(gameManager);
        InputManager.OnKeyPressed += TrySpecialAbility;
        InputManager.OnMouseDown += MouseDown;
        hpSlider = gameManager.GetStatusBar(0);
        abilitySlider = gameManager.GetStatusBar(1);
        hpSlider.gameObject.SetActive(true);
        abilitySlider.gameObject.SetActive(true);
        abilitySlider.maxValue = cooldown;
        abilitySlider.value = 0;
        hpSlider.maxValue = GetMaxHp();
        ui = gameManager.GetUiHandler();
    }

    public override void StatAdjustments()
    {
        base.StatAdjustments();
        hpSlider.maxValue = GetMaxHp();
    }

    public override float TakeDamage(float attack)
    {
        float currentHp = base.TakeDamage(attack);
        hpSlider.value = currentHp;
        return currentHp;
    }
    public override float Heal(float amount)
    {
        float hp = base.Heal(amount);
        hpSlider.maxValue = GetMaxHp();
        hpSlider.value = hp;
        return hp;
    }
    public virtual bool Validate()
    {
        return true;
    }

    public virtual void Update()
    {
        if (abilitySlider != null && abilitySlider.value < abilitySlider.maxValue)
        {
            abilitySlider.value += Time.deltaTime;
            if (abilitySlider.value > abilitySlider.maxValue)
            {
                abilitySlider.value = abilitySlider.maxValue;
            }
        }
    }
    void TrySpecialAbility(object sender, KeyEvent keyEvent)
    {
        if (keyEvent.keyPressed == abilityKey && abilitySlider.value >= abilitySlider.maxValue)
        {
            SpecialAbility();
        }
    }

    public virtual void SpecialAbility()
    {
        AudioSource.PlayClipAtPoint(specialAbilityNoise, transform.position);
        abilitySlider.value = 0;
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7 && collidedItem == null)
        {
            collidedItem = other.gameObject.GetComponent<Item>();
        }
    }

    public virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 7 && collidedItem.gameObject == other.gameObject)
        {
            collidedItem = null;
        }
    }

    void MouseDown(object sender, MouseEvent mouseEvent)
    {
        if (mouseEvent.mouseButton == 0)
        {
            if (collidedItem != null)
            {
                EquipItem(collidedItem);
            }
            else if (activeItem != null)
            {
                activeItem.Use(Level());
            }
        }
        else if (mouseEvent.mouseButton == 1)
        {
            if (activeItem != null)
            {
                DropItem();
            }
        }
    }

    void EquipItem(Item item)
    {
        if (activeItem != null)
        {
            activeItem.Drop();
        }
        activeItem = item;
        collidedItem = null;
        item.Equip(handPositionOffset, this);
    }
    void DropItem()
    {
        activeItem.Drop();
        activeItem = null;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        if (gameManager.GetUiHandler().CurrentMenu() != UiHandler.Menu.Win)
        {
            gameManager.GetUiHandler().SwitchMenu(UiHandler.Menu.GameOver);
        }

    }

    public virtual void SetMovementSpeed(float speed)
    {

    }

    public virtual float GetMovementSpeed()
    {
        return 0;
    }

}

