package org.example.controllers;

import lombok.RequiredArgsConstructor;
import org.example.dto.parentdto.ParentAddDto;
import org.example.dto.parentdto.ParentItemDto;
import org.example.dto.parentdto.ParentUpdateDto;
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
import java.util.List;

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
    @GetMapping("/getparent/{id}")
    @ResponseBody
    public ParentItemDto index(@PathVariable int id) {
        List<ParentItemDto> items = mapper.parentsToParentsAllDto(parentRepository.findAll());
        for (ParentItemDto item : items) {
            if (item.getId() == id) {
                return item;
            }
        }
        return null;
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

    @PutMapping("/update")
    public int updateParent(@RequestBody ParentUpdateDto parentUpdateDto) {
        parentRepository.findById(parentUpdateDto.getId())
                .map(parent1 -> {
                    parent1.setLastName(parentUpdateDto.getLastName());
                    parent1.setFirstName(parentUpdateDto.getFirstName());
                    parent1.setPhone(parentUpdateDto.getPhone());
                    parent1.setAdress(parentUpdateDto.getAdress());
                    String fileName = storageService.store(parentUpdateDto.getImageBase64());
                    parent1.setImage(fileName);
                    return parentRepository.save(parent1);
                });

        return 0;
    }
}
