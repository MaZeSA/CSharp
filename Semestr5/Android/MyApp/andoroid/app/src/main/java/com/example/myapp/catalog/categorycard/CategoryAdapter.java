package com.example.myapp.catalog.categorycard;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.myapp.R;
import com.example.myapp.application.HomeApplication;
import com.example.myapp.constans.Urls;
import com.example.myapp.dto.category.CategoryItemDTO;

import java.util.List;

public class CategoryAdapter extends RecyclerView.Adapter<CategoryCardViewHolder> {
    List<CategoryItemDTO> category;

    public CategoryAdapter(List<CategoryItemDTO> categorys) {
        this.category = categorys;
    }

    @NonNull
    @Override
    public CategoryCardViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View layoutView = LayoutInflater
                .from(parent.getContext())
                .inflate(R.layout.category_view, parent, false);
        return new CategoryCardViewHolder(layoutView);
    }

    @Override
    public void onBindViewHolder(@NonNull CategoryCardViewHolder holder, int position) {
        if(category !=null && position< category.size())
        {
            CategoryItemDTO category = this.category.get(position);
            String name = category.getName();
            holder.categoryname.setText(name);
            String url = Urls.BASE + category.getImage();
            Glide.with(HomeApplication.getInstance())
                    .load(url)
                    .apply(new RequestOptions().override(400))
                    .into(holder.categoryimage);
        }
    }

    @Override
    public int getItemCount() {
        return category.size();
    }
}
