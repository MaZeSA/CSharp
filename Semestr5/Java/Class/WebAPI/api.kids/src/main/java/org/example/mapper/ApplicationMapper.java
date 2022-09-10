package org.example.mapper;

import org.example.dto.authdto.UserView;
import org.example.dto.parentdto.ParentAddDto;
import org.example.dto.parentdto.ParentItemDto;
import org.example.dto.parentdto.ParentUpdateDto;
import org.example.entities.Parent;
import org.example.entities.UserEntity;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

import java.util.List;

@Mapper(componentModel = "spring")
public interface ApplicationMapper {
    Parent ParentByParentAddDto(ParentAddDto dto);
    ParentItemDto parentToParentItemDto(Parent Parent);
    List<ParentItemDto> parentsToParentsAllDto(List<Parent> parents);

    Parent ParentUpdateDtoByParent (ParentUpdateDto dto);

    @Mapping(target = "user.roles", ignore = true)
    UserView UserToUserView(UserEntity user);
}
