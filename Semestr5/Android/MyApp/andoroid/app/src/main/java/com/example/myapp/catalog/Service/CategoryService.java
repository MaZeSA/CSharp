package com.example.myapp.catalog.Service;


import com.example.myapp.constans.Urls;
import com.example.myapp.catalog.network.CategoriesApi;
import com.example.myapp.interceptors.JWTInterceptor;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class CategoryService {
    private static CategoryService mInstance;
    private static final String BASE_URL = Urls.BASE;
    private Retrofit mRetrofit;

    private CategoryService() {
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .connectTimeout(20, TimeUnit.SECONDS)
                .writeTimeout(20, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .addInterceptor(new JWTInterceptor())
                .build();
        mRetrofit = new Retrofit.Builder()
                .client(okHttpClient)
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
    }

    public static CategoryService getInstance() {
        if (mInstance == null) {
            mInstance = new CategoryService();
        }
        return mInstance;
    }
    public CategoriesApi getJSONApi() {
        return mRetrofit.create(CategoriesApi.class);
    }
}
