package com.example.myapp.account;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Base64;
import android.view.View;
import android.widget.ImageView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.myapp.BaseActivity;
import com.example.myapp.ChangeImageActivity;
import com.example.myapp.MainActivity;
import com.example.myapp.R;
import com.example.myapp.account.dto.AccountResponseDTO;
import com.example.myapp.account.dto.RegisterDTO;
import com.example.myapp.account.dto.ValidationRegisterDTO;
import com.example.myapp.account.network.AccountService;
import com.example.myapp.application.HomeApplication;
import com.example.myapp.utils.CommonUtils;
import com.google.android.material.textfield.TextInputEditText;
import com.google.android.material.textfield.TextInputLayout;
import com.google.gson.Gson;

import java.io.ByteArrayOutputStream;
import java.io.IOException;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RegisterActivity  extends BaseActivity {

    int SELECT_PICTURE = 200;
    int SELECT_CROPPER = 300;
    String sImage="";

    ImageView IVPreviewImage;

    private TextInputLayout textFieldLogin;
    private TextInputEditText txtLogin;
    private TextInputLayout textFieldPassword;
    private TextInputEditText txtPassword;
    private TextInputLayout textFieldConfimPassword;
    private TextInputEditText txtConfimPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        textFieldLogin = findViewById(R.id.textFieldLogin);
        txtLogin = findViewById(R.id.txtLogin);

        txtPassword = findViewById(R.id.txtPassword);

        textFieldConfimPassword = findViewById(R.id.textFieldConfimPassword);
        txtConfimPassword = findViewById(R.id.txtConfimPassword);

        IVPreviewImage = findViewById(R.id.IVPreviewImage);
    }

    public void handleSelectImageClick(View view) {
        Intent intent = new Intent(this, ChangeImageActivity.class);
        startActivityForResult(intent, SELECT_CROPPER);
    }
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (resultCode == SELECT_CROPPER) {
            sImage = data.getStringExtra("base64");
            byte[] imageByteArray = Base64.decode(sImage, Base64.DEFAULT);
            Glide.with(HomeApplication.getInstance())
                    .load(imageByteArray)
                    .apply(new RequestOptions().override(600,300))
                    .into(IVPreviewImage);
        }
//        if(resultCode == SELECT_CROPPER)
//        {
//            String base64 = data.getStringExtra("base64");
//        }
//        if (resultCode == RESULT_OK) {
//
//            // compare the resultCode with the
//            // SELECT_PICTURE constant
//            if (requestCode == SELECT_PICTURE) {
//                // Get the url of the image from data
//                Uri uri = data.getData();
//                // update the preview image in the layout
//                //IVPreviewImage.setImageURI(uri);
//                Bitmap bitmap= null;
//                try {
//                    bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(),uri);
//                } catch (IOException e) {
//                    e.printStackTrace();
//                }
//                // initialize byte stream
//                ByteArrayOutputStream stream=new ByteArrayOutputStream();
//                // compress Bitmap
//                bitmap.compress(Bitmap.CompressFormat.JPEG,100,stream);
//                // Initialize byte array
//                byte[] bytes=stream.toByteArray();
//                // get base64 encoded string
//                sImage= Base64.encodeToString(bytes,Base64.DEFAULT);
//            }
//        }
    }

    public void handleClickRegister(View view) {
        RegisterDTO registerDTO = new RegisterDTO();
        registerDTO.setEmail(txtLogin.getText().toString());
        registerDTO.setPassword(txtPassword.getText().toString());
        registerDTO.setConfirmPassword(txtConfimPassword.getText().toString());
        registerDTO.setFirstName("test");
        registerDTO.setSecondName("test");
        registerDTO.setPhoto(sImage);

        CommonUtils.showLoading();

        if (!validationFields(registerDTO))
            return;
        AccountService.getInstance()
                .jsonApi()
                .register(registerDTO)
                .enqueue(new Callback<AccountResponseDTO>() {
                    @Override
                    public void onResponse(Call<AccountResponseDTO> call, Response<AccountResponseDTO> response) {
                        CommonUtils.hideLoading();
                        if (response.isSuccessful()) {
                            AccountResponseDTO data = response.body();
                            //JwtSecurityService jwtService = (JwtSecurityService) HomeApplication.getInstance();
                            //jwtService.saveJwtToken(data.getToken());
                            //tvInfo.setText("response is good");
                            Intent intent = new Intent(RegisterActivity.this, MainActivity.class);
                            startActivity(intent);
                            finish();
                        } else {
                            try {
                                showErrorsServer(response.errorBody().string());
                            } catch (Exception e) {
                                System.out.println("------Error response parse body-----");
                            }
                        }
                    }

                    @Override
                    public void onFailure(Call<AccountResponseDTO> call, Throwable t) {
                        String str = t.toString();
                        int a = 12;
                    }
                });
    }

    private boolean validationFields(RegisterDTO registerDTO) {
        textFieldLogin.setError("");
        if (registerDTO.getEmail().equals("")) {
            textFieldLogin.setError("Вкажіть Email");
            return false;
        }
        if (!registerDTO.getPassword().equals(registerDTO.getConfirmPassword())) {
            textFieldConfimPassword.setError("Паролі не співпадають");
            return false;
        }

        return true;
    }

    private void showErrorsServer(String json) {
        Gson gson = new Gson();
        ValidationRegisterDTO result = gson.fromJson(json, ValidationRegisterDTO.class);
        String str = "";
        if (result.getErrors().getEmail() != null) {
            for (String item : result.getErrors().getEmail()) {
                str += item + "\n";
            }
        }
        textFieldLogin.setError(str);
    }

}
