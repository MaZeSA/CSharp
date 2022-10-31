package com.example.myapp.catalog;

import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Base64;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.myapp.BaseActivity;
import com.example.myapp.ChangeImageActivity;
import com.example.myapp.MainActivity;
import com.example.myapp.R;
import com.example.myapp.account.LoginActivity;
import com.example.myapp.account.dto.RegisterDTO;
import com.example.myapp.application.HomeApplication;
import com.example.myapp.catalog.Service.CategoryNetwork;
import com.example.myapp.dto.category.CreateCategoryDTO;
import com.example.myapp.dto.category.CreateCategoryResultDTO;
import com.example.myapp.catalog.Service.CategoryService;
import com.example.myapp.utils.CommonUtils;
import com.google.android.material.textfield.TextInputEditText;
import com.google.android.material.textfield.TextInputLayout;

import java.io.ByteArrayOutputStream;
import java.io.IOException;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CaregoryAddActivity extends BaseActivity {

    public ImageView userimage;
    private TextView imageError;
    private TextInputLayout textFieldCategoryName;
    private TextInputEditText textFieldName;
    private TextInputLayout textFieldPriority;
    private TextInputEditText txtFieldPriority;
    private EditText txtDescription;
    private ImageButton imageButton;

    int SELECT_CROPPER = 300;
    String base64;
    Uri uri;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_caregory_add);

        userimage=findViewById(R.id.userimage);
        imageError = findViewById(R.id.textImageError);

        textFieldCategoryName = findViewById(R.id.textFieldCategory);
        textFieldName= findViewById(R.id.txtCategory);

        textFieldPriority = findViewById(R.id.textFieldPriority);
        txtFieldPriority= findViewById(R.id.txtPriority);

        txtDescription= findViewById(R.id.txtDescription);
    }

    public void handleSelectImageClick(View view) {
        Intent intent = new Intent(this, ChangeImageActivity.class);
        startActivityForResult(intent, SELECT_CROPPER);
    }
public void onActivityResult(int requestCode, int resultCode, Intent data) {
    super.onActivityResult(requestCode, resultCode, data);

    if (resultCode == SELECT_CROPPER) {
        //String base64 = data.getStringExtra("base64");
        uri = (Uri) data.getParcelableExtra("croppedUri");
        userimage.setImageURI(uri);
        int a = 12;
        a = 16;
    }
}
    public void handleCreateCategory(View view)
    {
        CommonUtils.setContext(this);

        CreateCategoryDTO categoryCreateDTO=new CreateCategoryDTO();
        categoryCreateDTO.setNameCategory(textFieldName.getText().toString());
        categoryCreateDTO.setImageBase64(uriGetBase64(uri));
        try {
            categoryCreateDTO.setPriority(Integer.parseInt(txtFieldPriority.getText().toString()));
       }catch(NumberFormatException ee) {
            categoryCreateDTO.setPriority(-1);
       }
        categoryCreateDTO.setDescription(txtDescription.getText().toString());
        if(validationFields(categoryCreateDTO))
       {
        CommonUtils.showLoading();
        CategoryNetwork
                .getInstance()
                .getJSONApi()
                .create(categoryCreateDTO)
                .enqueue(new Callback<CreateCategoryResultDTO>() {
                    @Override
                    public void onResponse(Call<CreateCategoryResultDTO> call, Response<CreateCategoryResultDTO> response) {
                    CommonUtils.hideLoading();
                        Intent intent = new Intent(CaregoryAddActivity.this, CatalogActivity.class);
                        startActivity(intent);
                        finish();
                    }

                    @Override
                    public void onFailure(Call<CreateCategoryResultDTO> call, Throwable t) {
                        CommonUtils.hideLoading();
                    }
                });
       }
    }
    private String uriGetBase64(Uri uri)
    {
        try{
            Bitmap bitmap= null;
            try {
                bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(),uri);
            } catch (IOException e) {
                e.printStackTrace();
            }
            // initialize byte stream
            ByteArrayOutputStream stream=new ByteArrayOutputStream();
            // compress Bitmap
            bitmap.compress(Bitmap.CompressFormat.JPEG,100,stream);
            // Initialize byte array
            byte[] bytes=stream.toByteArray();
            // get base64 encoded string
            String sImage= Base64.encodeToString(bytes, Base64.DEFAULT);
            return sImage;
        }
        catch (Exception ex) {
            return null;
        }
    }
    private boolean validationFields(CreateCategoryDTO createCategoryDTO) {
        textFieldCategoryName.setError("");
        if (createCategoryDTO.getNameCategory().equals("")) {
            textFieldCategoryName.setError("Вкажіть Назву категорії");
            return false;
        }
        if (createCategoryDTO.getImageBase64() == null) {
            imageError.setVisibility(View.VISIBLE);
            return false;
        }
        if (createCategoryDTO.getPriority() < 0 ) {
            textFieldPriority.setError("Приорітет не вірний");
            return false;
        }
        return true;
    }
}
