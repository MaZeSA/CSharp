package com.example.myapp;

import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;

import androidx.appcompat.app.AppCompatActivity;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.myapp.Service.CategoryNetwork;
import com.example.myapp.application.HomeApplication;
import com.example.myapp.constans.Urls;
import com.example.myapp.dto.category.CategoryItemDTO;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    ArrayList<CategoryItemDTO> categoryItems = new ArrayList<CategoryItemDTO>();
    CategoryAdapter categoryAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView((R.layout.activity_main));

        categoryAdapter = new CategoryAdapter(this, R.layout.category_view, categoryItems);
        ListView categoryList = findViewById(R.id.categoryList);
        categoryList.setAdapter(categoryAdapter);

        requestServer();

        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

    }

    private void requestServer()
    {
        CategoryNetwork.getInstance().getJSONApi().list().enqueue(new Callback<List<CategoryItemDTO>>() {
            @Override
            public void onResponse(Call<List<CategoryItemDTO>> call, Response<List<CategoryItemDTO>> response) {
                List<CategoryItemDTO> data = response.body();
                for (CategoryItemDTO cat: data) {
                    categoryItems.add(cat);
                }
            }
            @Override
            public void onFailure(Call<List<CategoryItemDTO>> call, Throwable t) {

            }
        });
    }
}