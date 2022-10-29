package com.example.myapp.dto.category;

public class CreateCategoryDTO {
        public String name;
        public String imageBase64;
        public int priority;
        public String description;

        public String getNameCategory() {
                return name;
        }

        public void setNameCategory(String nameCategory) {
                this.name = nameCategory;
        }

        public String getImageBase64() {
                return imageBase64;
        }

        public void setImageBase64(String imageBase64) {
                this.imageBase64 = imageBase64;
        }

        public int getPriority() {
                return priority;
        }

        public void setPriority(int priority) {
                this.priority = priority;
        }

        public String getDescription() {
                return description;
        }

        public void setDescription(String description) {
                this.description = description;
        }


}
