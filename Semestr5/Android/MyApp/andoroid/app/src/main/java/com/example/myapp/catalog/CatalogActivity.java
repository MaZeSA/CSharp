package com.example.myapp.catalog;

import android.os.Bundle;

import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.myapp.BaseActivity;
import com.example.myapp.R;
import com.example.myapp.catalog.Service.CategoryService;
import com.example.myapp.catalog.categorycard.CategoryAdapter;
import com.example.myapp.dto.category.CategoryItemDTO;
import com.example.myapp.utils.CommonUtils;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CatalogActivity extends BaseActivity {
    private RecyclerView rcvCategory;
    private CategoryAdapter adapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_catalog);

        rcvCategory = findViewById(R.id.rcvCategory);
        rcvCategory.setHasFixedSize(true);
        rcvCategory.setLayoutManager(new GridLayoutManager(this, 1,
                RecyclerView.VERTICAL, false));
        CommonUtils.showLoading();

        CategoryService.getInstance()
                .jsonApi()
                .list()
                .enqueue(new Callback<List<CategoryItemDTO>>() {
                    @Override
                    public void onResponse(Call<List<CategoryItemDTO>> call, Response<List<CategoryItemDTO>> response) {
                        if(response.isSuccessful())
                        {
                            adapter=new CategoryAdapter(response.body());
                            rcvCategory.setAdapter(adapter);
                        }
                        CommonUtils.hideLoading();
                    }

                    @Override
                    public void onFailure(Call<List<CategoryItemDTO>> call, Throwable t) {

                    }
                });
    }
}