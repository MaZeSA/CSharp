package com.example.myapp.Service;

import com.example.myapp.constans.Urls;
import com.example.myapp.network.CategoriesApi;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class CategoryService {
    private static CategoryService instance;
        private Retrofit retrofit;

        private CategoryService() {
            OkHttpClient okHttpClient = new OkHttpClient.Builder()
                    .connectTimeout(20, TimeUnit.SECONDS)
                    .writeTimeout(20, TimeUnit.SECONDS)
                    .readTimeout(30, TimeUnit.SECONDS)
//                    .addInterceptor(new JWTInterceptor())
                    .build();
            retrofit = new Retrofit.Builder()
                    .client(okHttpClient)
                    .baseUrl(Urls.BASE)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }

        public static CategoryService getInstance() {
            if(instance==null)
                instance=new CategoryService();
            return instance;
        }

        public CategoriesApi jsonApi() {
            return retrofit.create(CategoriesApi.class);
        }


}