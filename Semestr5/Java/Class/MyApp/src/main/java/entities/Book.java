package entities;

import lombok.Data;

import javax.persistence.*;
import java.util.Scanner;

@Data
@Entity
@Table(name = "tbl_books")
public class Book implements IEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    @Column(length = 200, nullable = false)
    private String name;
    @ManyToOne
    @JoinColumn(name = "author_id", nullable = false)
    private  Author author;

    static Scanner in = new Scanner(System.in, "UTF-8");

    @Override
    public IEntity Update() {
        System.out.println("Актуальна назва: " + this.getName() + "\n Введіть нову назву");
        this.setName(in.next());
        System.out.println("Актуальний автор: " + author.getFullName() + "\n Введіть ід нового автора");
        Author author = new Author();
        author.setId(in.nextInt());
        this.setAuthor(author);
        return this;
    }
    @Override
    public void Print() {
        System.out.println(this.getId() + " " + this.getName());
    }
    public static IEntity GetNew() {
        System.out.println("Введіть імя книги");
        Book book = new Book();
        book.setName(in.next());

        System.out.println("Введіть ід автора книги");
        Author autor = new Author();
        autor.setId(in.nextInt());
        book.setAuthor(autor);
        return book;
    }
}
