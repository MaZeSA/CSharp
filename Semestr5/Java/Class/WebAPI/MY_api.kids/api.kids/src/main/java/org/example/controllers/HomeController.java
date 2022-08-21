package org.example.controllers;

import lombok.RequiredArgsConstructor;
import org.example.entities.CreateParent;
import org.example.entities.Parent;
import org.example.repositories.ParentRepository;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import javax.imageio.ImageIO;
import javax.xml.bind.DatatypeConverter;
import java.awt.image.BufferedImage;
import java.io.*;
import java.util.List;
import java.util.UUID;

@RestController
@RequiredArgsConstructor
public class HomeController {
    private  final ParentRepository parentRepository;
    @GetMapping("/")
    public List<Parent> index(){
        return parentRepository.findAll();
    }

    @PostMapping(path = "/create",
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE)
    public int addParent(@RequestBody CreateParent newParent){
        System.out.println("set " + newParent.getFirstName());

        if(newParent.getFirstName() == null)
            return 0;

        Parent parent = new Parent();
        parent.setFirstName(newParent.getFirstName());
        parent.setLastName(newParent.getLastName());
        parent.setPhone(newParent.getPhone());
        parent.setAdress(newParent.getAdress());

        String img = SeveImage(newParent.getImage());
        parent.setImage(img);

        parentRepository.save(parent);
        System.out.println("Saved!");
        return 1;
    }

    private String SeveImage(String base64img){
        if(base64img !="") {
            String[] base64Image = base64img.split(",");
            byte[] data = DatatypeConverter.parseBase64Binary(base64Image[1]);
            String extension;
            switch (base64Image[0]) {//check image's extension
                case "data:image/jpeg;base64":
                    extension = ".jpeg";
                    break;
                case "data:image/png;base64":
                    extension = ".png";
                    break;
                default://should write cases for more images types
                    extension = ".jpg";
                    break;
            }
            UUID uuid = UUID.randomUUID();
            String nameFile = uuid.toString();

            String path = "C:\\api.kids\\" + nameFile + extension;
            File file = new File(path);
            try (OutputStream outputStream = new BufferedOutputStream(new FileOutputStream(file))) {
                outputStream.write(data);
                base64img =nameFile + extension;
            } catch (IOException e) {
                e.printStackTrace();
                base64img = "";
            }
        }
        else {
            base64img = "";
        }
        return  base64img;
    }

    @RequestMapping(
            path = "/update",
            method = RequestMethod.PUT,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE)
    public int updateParent(Parent newParent) {
        parentRepository.findById(newParent.getId())
                .map(parent1 -> {
                    parent1.setLastName(newParent.getLastName());
                    parent1.setFirstName(newParent.getFirstName());
                    parent1.setPhone(newParent.getPhone());
                    parent1.setAdress(newParent.getAdress());

                    String img = SeveImage(newParent.getImage());
                    parent1.setImage(img);
                    return parentRepository.save(parent1);
                });

        return 0;
    }

    @RequestMapping(method = RequestMethod.DELETE)
    public int deleteParent(int id) {
        parentRepository.deleteById(id);
        return 0;
    }
}
