package org.example.controllers;

import lombok.RequiredArgsConstructor;
import org.example.dto.parentdto.ParentAddDto;
import org.example.dto.parentdto.ParentItemDto;
import org.example.dto.parentdto.ParentUpdateDto;
import org.example.dto.parentdto.SearchDto;
import org.example.entities.Parent;
import org.example.mapper.ApplicationMapper;
import org.example.repositories.ParentRepository;
import org.example.storage.StorageService;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import org.springframework.core.io.Resource;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@RestController
@RequiredArgsConstructor
public class HomeController {

    private final ApplicationMapper mapper;
    private final StorageService storageService;
    private final ParentRepository parentRepository;
    @GetMapping("/")
    public List<ParentItemDto> index() {
        List<ParentItemDto> items =  mapper.parentsToParentsAllDto(parentRepository.findAll());
        return items;
    }

    @PostMapping("/create")
    public String add(@RequestBody ParentAddDto parentAddDto) {
        Parent parent = mapper.ParentByParentAddDto(parentAddDto);
        String fileName = storageService.store(parentAddDto.getImageBase64());
        parent.setImage(fileName);
        parentRepository.save(parent);
        return fileName;
    }
    @GetMapping("/files/{filename:.+}")
    @ResponseBody
    public ResponseEntity<Resource> serveFile(@PathVariable String filename) throws Exception {

        Resource file = storageService.loadAsResource(filename);
        String urlFileName =  URLEncoder.encode("сало.jpg", StandardCharsets.UTF_8.toString());
        return ResponseEntity.ok()
                //.header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + file.getFilename() + "\"").body(file);
                .contentType(MediaType.IMAGE_JPEG)

                .header(HttpHeaders.CONTENT_DISPOSITION,"filename=\""+urlFileName+"\"")
                .body(file);
    }

    @DeleteMapping("/remove/{id}")
    public int deleteParent(@PathVariable int id) {
        Optional<Parent> p = parentRepository.findById(id);
        storageService.removeFile(p.get().getImage());
        parentRepository.deleteById(id);
        return 0;
    }

    @PutMapping("/update")
    public int updateParent(@RequestBody ParentUpdateDto newParent) {
        parentRepository.findById(newParent.getId())
                .map(parent1 -> {
                    parent1.setLastName(newParent.getLastName());
                    parent1.setFirstName(newParent.getFirstName());
                    parent1.setPhone(newParent.getPhone());
                    parent1.setAdress(newParent.getAdress());

                    storageService.removeFile(parent1.getImage());
                    String fileName = storageService.store(newParent.getImageBase64());
                    parent1.setImage(fileName);
                    return parentRepository.save(parent1);
                });

        return 0;
    }

    @GetMapping("/search")
    @ResponseBody
    public List<ParentItemDto> searchParents(@RequestParam int id, @RequestParam String name) {
        List<Parent> items = new ArrayList<Parent>(){};

       for (Parent item: parentRepository.findAll() ) {
            if(item.getFirstName().equals(name))
                items.add(item);
        }
        Optional<Parent> p = parentRepository.findById(id);
        if(!p.isEmpty()){
            items.add(p.get());
        }

        return mapper.parentsToParentsAllDto(items);
    }
}
