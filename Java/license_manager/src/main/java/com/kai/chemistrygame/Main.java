package com.kai.chemistrygame;

import com.kai.chemistrygame.Persistense.postgres.PostgresInit;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
@SpringBootApplication
public class Main {
    public static void main(String[] args) {
        Integer resultInit = PostgresInit.DatabaseInit("license_manager", "postgres", "salavat");
        if(resultInit == 0) {
            SpringApplication.run(Main.class, args);
        } else {
            System.out.println("Database doesn't exist!");
        }
    }

}
