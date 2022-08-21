import * as yup from "yup";

export const CategorySchema = yup.object({
  firstName: yup
    .string()
    .required("Поле пошта є обов'язковим!")
});
