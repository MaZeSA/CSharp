package org.example;

import entities.Author;
import entities.Book;
import entities.IEntity;
import entities.Role;

import java.io.PrintStream;
import java.util.List;
import java.util.Scanner;
import org.hibernate.Session;
import org.hibernate.query.Query;
import utils.HibernateSessionFactoryUtil;


public class Main {
    static Scanner in = new Scanner(System.in, "UTF-8");

    public static void main(String[] args) throws Exception {
        System.setOut(new PrintStream(System.out, true, "UTF-8"));

        int action =-1;
        int subAction =-1;

        while (action !=0) {
            System.out.println("Виберіть дію: \n1.Книги \n2.Ролі\n0.Вихід");
            action = in.nextInt();

            while (action==1) {
                System.out.println("Меню книжок: \n1.Список книг \n2.Додати книгу\n3.Редагувати книгу\n4.Видалити книгу\n0.Назад");
                subAction = in.nextInt();
                switch (subAction){
                    case 1: {
                        ShowEntity("Book");
                        break;
                    }
                    case 2: {
                        AddEntity(Book.GetNew());
                        break;
                    }
                    case 3: {
                        UpdateEntity("Book");
                        break;
                    }
                    case 4: {
                        RemoveEntity("Book");
                        break;
                    }
                    case 0: {
                        action =-1;
                        break;
                    }
                }
            }
            while (action==2) {
                System.out.println("Меню ролей: \n1.Список ролів \n2.Додати роль\n3.Редагувати роль\n4.Видалити роль\n0.Назад");
                subAction = in.nextInt();
                switch (subAction) {
                    case 1: {
                        ShowEntity("Role");
                        break;
                    }
                    case 2: {
                        AddEntity(Role.GetNew());
                        break;
                    }
                    case 3: {
                        UpdateEntity("Role");
                        break;
                    }
                    case 4: {
                        RemoveEntity("Role");
                        break;
                    }
                    case 0:{
                        action =-1;
                        break;
                    }
                }
            }
        }
        System.out.println("Закрито!");
//        for(Book b : roles){
    }

    private static void ShowEntity(String entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Query query = session.createQuery("FROM " + entity);
        List<IEntity> entities = query.list();
        for (IEntity e : entities) {
            e.Print();
        }
        session.close();
    }
    private static void AddEntity(IEntity entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        session.save(entity);
        session.close();
        System.out.println("Сутність збережена");
    }
    private static void UpdateEntity(String table){
        System.out.println("Введіть id");
        int id = in.nextInt();

        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Query query = session.createQuery("FROM "+ table +" WHERE id=" + id);
        if(query != null) {
            IEntity entity = (IEntity) query.getSingleResult();
            entity.Update();

            session.beginTransaction();
            session.update(entity);
            session.getTransaction().commit();
            System.out.println("Оновлено!");
        }
        else{
            System.out.println("Не знайдено!");
        }
        session.close();
    }
    private static void RemoveEntity(String table){
        System.out.println("Введіть id");
        int id = in.nextInt();

        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Query query = session.createQuery("FROM "+ table +" WHERE id=" + id);
        if(query != null) {
            IEntity role = (IEntity) query.getSingleResult();
            session.beginTransaction();
            session.delete(role);
            session.getTransaction().commit();
            System.out.println(" Видалено!");
        }
        else{
            System.out.println("Не знайдено!");
        }
        session.close();
    }
}