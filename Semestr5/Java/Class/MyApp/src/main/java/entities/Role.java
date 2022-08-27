package entities;


import lombok.Data;

import javax.persistence.*;
import java.util.Scanner;

@Data
@Entity
@Table(name="tbl_roles")
public class Role implements IEntity{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    @Column(length = 250, nullable = false)
    private String name;
    static Scanner in = new Scanner(System.in, "UTF-8");

    @Override
    public void Print() {
        System.out.println(this.getId() + " " + this.getName());
    }
    @Override
    public IEntity Update() {
        System.out.println("Актуальне імя: "+ this.getName() + "\n Введіть нове імя");
        this.setName(in.next());
        return this;
    }
    public static IEntity GetNew() {
        System.out.println("Введіть імя ролі");
        String name = in.next();
        Role role = new Role();
        role.setName(name);
        return role;
    }
}
