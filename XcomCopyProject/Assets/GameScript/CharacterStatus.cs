﻿using System;

[Serializable]
public struct CharacterStatus
{
    public int hp;
    public float moveRange;
    public int movePoint;
    public int attackPoint;
    public string model;
    public Weapon weapon;
}