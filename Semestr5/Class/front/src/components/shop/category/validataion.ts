import * as yup from "yup";

export const CategorySchema = yup.object({
  name: yup
    .string()
    .required("Поле пошта є обов'язковим!")
});
