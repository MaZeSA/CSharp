import * as yup from "yup";

export const RegisterSchema = yup.object({
  email: yup
    .string()
    .required("Поле пошта є обов'язковим!")
    .email("Вкажіть привильно пошту"),
  photo: yup
    .string()
    .required("Оберіть фото. Щоб обрати натисніть на зображення!"),
  firstName: yup
  .string()
  .required("Поле імя є обов'язковим!"),
  phone: yup
  .string()
  .matches(/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im , 'Не коректний формат'),
  password: yup
  .string()
  .matches(/^[A-Za-z0-9]\w{8,}$/,"Пароль повинен містити не меньше 8 символів")
  .required("Поле пароль є обов'язковим!")
});
