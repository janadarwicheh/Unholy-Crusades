using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Godot;
using Skull.Scenes.Entities.Skills;

namespace Skull.Scenes.Entities;

using System.Collections.Generic;
using System.Linq;
using Godot;

public class HitBox
{
    
    //Chaque capacité/atttaque aura une hitbox. Chacune sera définie dans des fichiers différents en fonction de l'appartenance. Les personnages auront donc une liste de capacités, et ces capaicités invoqueront leur hitbox respective. 
    public Skill Skill { get; set; }
    public Dictionary<int, CollisionShape2D> CorrespondingHitboxes { get; set; }
    public int Frames { get; set; }
    //Le but sera de faire un dictionnaire stockant les collision shapes correspondant à chaque area2D en les récupérant à l'aide de GetChildren(). après les avoir récupérées et mises dans le dico,
    //on les lira à l'aide d'un autre dictionnaire prenant en clé les animations et renvoyant les area2D correspondantes pour aller les chercher dans CorrespondingHitboxes. 

    public HitBox(Skill skill, int[] frameOrder = null)
    {
        Skill = skill;
        var a = Skill.User.FindChild(Skill.Name);
        if (a != null && frameOrder != null)
        {
            CorrespondingHitboxes = new Dictionary<int, CollisionShape2D>();
            Skill.HitBox = this;
            Frames = frameOrder.Length;
            var shapes = a.GetChildren();
            if (frameOrder.Max() > shapes.Count)
            {
                throw new Exception("Frame order is too high for the number of hitboxes.");
            }

            for (int i = 0; i < Frames; i++)
            {
                CorrespondingHitboxes.Add(i, (CollisionShape2D)shapes[i]);
            }
        }
    }

    public void Use()
    {
        if (Skill.Launch())
        {
            int cpt = 0;
            bool cFrame = CurrentInfo.FrameCounter;
            while (cpt < Frames)
            {
                if (cFrame != CurrentInfo.FrameCounter)
                {
                    CorrespondingHitboxes[cpt].Disabled = true;
                    cpt++;
                }
                CorrespondingHitboxes[cpt].Disabled = false;
            }
        }
    }
    
}

