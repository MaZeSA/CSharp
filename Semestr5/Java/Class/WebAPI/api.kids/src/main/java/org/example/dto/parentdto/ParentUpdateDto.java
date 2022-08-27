package org.example.dto.parentdto;

import lombok.Data;

import javax.persistence.Column;

@Data
public class ParentAddDto {
    private String imageBase64;
    private String firstName;
    private String lastName;
    private String phone;
    private String adress;
}
