package com.kai.chemistrygame.Persistense.postgres;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.sql.*;
public class PostgresInit {
    public static Integer DatabaseInit(String DatabaseName, String PostgresUsername, String PostgresPassword) {
        Integer result = 1;
        Logger logger = LoggerFactory.getLogger(PostgresInit.class);
        Connection connection = null;
        Statement statement = null;
        try {
            logger.debug("Checking database...");
            connection = DriverManager.getConnection("jdbc:postgresql://localhost:5432/", PostgresUsername, PostgresPassword);
            statement = connection.createStatement();
            statement.executeQuery(String.format("SELECT count(*) FROM pg_database WHERE datname = '%s'", DatabaseName));
            ResultSet resultSet = statement.getResultSet();
            resultSet.next();
            int count = resultSet.getInt(1);

            if (count <= 0) {
                logger.debug("Create database manually, name in application.yml file");
                System.out.println("Create database manually, name in application.yml file");
            } else {
                logger.debug("Database already exist.");
                result = 0;
            }
        } catch(SQLException exception) {
            logger.error(exception.toString());
        } finally {
            try {
                if (statement != null) {
                    statement.close();
                    logger.debug("Closed Statement.");
                }
                if (connection != null) {
                    logger.debug("Closed Connection.");
                    connection.close();
                }
            } catch (SQLException e) {
                logger.error(e.toString());
            }
        }
        return result;
    }
}
