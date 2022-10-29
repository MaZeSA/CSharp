package com.example.myapp.catalog;

import android.content.Intent;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.myapp.BaseActivity;
import com.example.myapp.ChangeImageActivity;
import com.example.myapp.MainActivity;
import com.example.myapp.R;
import com.example.myapp.account.LoginActivity;
import com.example.myapp.application.HomeApplication;
import com.example.myapp.dto.category.CreateCategoryDTO;
import com.example.myapp.dto.category.CreateCategoryResultDTO;
import com.example.myapp.catalog.Service.CategoryService;
import com.example.myapp.utils.CommonUtils;
import com.google.android.material.textfield.TextInputEditText;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CaregoryAddActivity extends BaseActivity {

    public ImageView userimage;
    private TextInputEditText textFieldName;
    private TextInputEditText txtFieldPriority;
    private EditText txtDescription;
    private ImageButton imageButton;

    int SELECT_CROPPER = 300;
    String base64;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_caregory_add);
        userimage=findViewById(R.id.userimage);
        textFieldName= findViewById(R.id.txtCategory);
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
            base64 = data.getStringExtra("base64");
            byte[] imageByteArray = Base64.decode(base64, Base64.DEFAULT);
            Glide.with(HomeApplication.getInstance())
                    .load(imageByteArray)
                    .apply(new RequestOptions().override(600,300))
                    .into(userimage);
        }
    }
    public void handleCreateCategory(View view) {

        CreateCategoryDTO newCategory = new CreateCategoryDTO();
        newCategory.setNameCategory(String.valueOf(textFieldName.getText().toString()));
        newCategory.setImageBase64(base64);
        newCategory.setPriority(Integer.parseInt(txtFieldPriority.getText().toString()));
        newCategory.setDescription(txtDescription.getText().toString());
        CommonUtils.showLoading();

        CategoryService.getInstance()
                .jsonApi()
                .create(newCategory)
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

                    }
                });
    }
}