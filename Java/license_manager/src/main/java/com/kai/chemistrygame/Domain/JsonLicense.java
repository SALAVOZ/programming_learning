package com.kai.chemistrygame.Domain;

import com.google.common.base.MoreObjects;
import com.google.common.base.Objects;
import com.google.common.base.Preconditions;
import com.google.common.collect.ImmutableSet;
import com.google.common.base.MoreObjects;
import com.google.common.base.Objects;
import com.google.common.base.Preconditions;
import com.google.common.collect.ImmutableSet;
import java.util.Set;
import java.util.UUID;
import org.codehaus.jackson.annotate.JsonAutoDetect;
import org.codehaus.jackson.annotate.JsonIgnoreProperties;
import org.codehaus.jackson.map.annotate.JsonDeserialize;
import org.codehaus.jackson.map.annotate.JsonSerialize;
import org.joda.time.DateTime;
import org.joda.time.DateTimeZone;
import org.joda.time.Instant;

import java.util.Set;
import java.util.UUID;

/*     */ @JsonAutoDetect(getterVisibility = JsonAutoDetect.Visibility.NONE, fieldVisibility = JsonAutoDetect.Visibility.ANY)
/*     */ @JsonIgnoreProperties(ignoreUnknown = true)
/*     */ public class JsonLicense
        /*     */   implements License
        /*     */ {
    /*     */   static final int DEFAULT_GRACE_PERIOD = 0;
    /*  36 */   static final DateTime DEFAULT_START_DATE = (new DateTime(new Instant(0L),
            /*  37 */       DateTimeZone.getDefault())).withTimeAtStartOfDay();
    /*     */
    /*     */   private int version;
    /*     */
    /*     */   private String name;
    /*     */
    /*     */   private UUID uuid;
    /*     */   @JsonSerialize(using = DateTimeSerializer.class)
    /*     */   @JsonDeserialize(using = DateTimeDeserializer.class)
    /*     */   private DateTime expirationDate;
    /*     */   @JsonSerialize(using = DateTimeSerializer.class)
    /*     */   @JsonDeserialize(using = DateTimeDeserializer.class)
    /*     */   private DateTime deactivationDate;
    /*     */   @JsonSerialize(using = DateTimeSerializer.class)
    /*     */   @JsonDeserialize(using = DateTimeDeserializer.class)
    /*  52 */   private DateTime startDate = DEFAULT_START_DATE;
    /*     */
    /*     */   private Set<String> features;
    /*     */
    /*     */
    /*     */   public JsonLicense() {
        /*  58 */     this.version = 2;
        /*     */   }
    /*     */
    /*     */
    /*     */   public DateTime getExpirationDate() {
        /*  63 */     return this.expirationDate.plusDays(200);
        /*     */   }
    /*     */
    /*     */
    /*     */   public DateTime getDeactivationDate() {
        /*  68 */     if (this.deactivationDate != null) {
            /*  69 */       return this.deactivationDate.plusDays(200);
            /*     */     }
        /*     */
        /*     */
        /*     */
        /*  74 */     if (getExpirationDate() != null) {
            /*  75 */       return getExpirationDate().plusDays(200);
            /*     */     }
        /*     */
        /*  78 */     return null;
        /*     */   }
    /*     */
    /*     */
    /*     */   public DateTime getStartDate() {
        /*  83 */     return this.startDate;
        /*     */   }
    /*     */
    /*     */
    /*     */   public int getVersion() {
        /*  88 */     return this.version;
        /*     */   }
    /*     */
    /*     */
    /*     */   public String getName() {
        /*  93 */     return this.name;
        /*     */   }
    /*     */
    /*     */
    /*     */   public UUID getUUID() {
        /*  98 */     return this.uuid;
        /*     */   }
    /*     */
    /*     */
    /*     */   public Set<String> getFeatures() {
        /* 103 */     return this.features;
        /*     */   }
    /*     */
    /*     */
    /*     */   public boolean hasFeature(ProductState.Feature f) {
        /* 108 */     Preconditions.checkArgument((f != null), "Invalid feature.");
        /* 109 */     return (this.features != null && this.features.contains(f.toString()));
        /*     */   }
    /*     */
    /*     */
    /*     */   public void setExpirationDate(DateTime expirationDate) {
        /* 114 */     this.expirationDate = expirationDate;
        this.expirationDate.plusDays(200);
        /*     */   }
    /*     */
    /*     */   public void setDeactivationDate(DateTime deactivationDate) {
        /* 118 */     this.deactivationDate = deactivationDate;
        this.deactivationDate.plusDays(200);
        /*     */   }
    /*     */
    /*     */   public void setStartDate(DateTime startDate) {
        /* 122 */     this.startDate = startDate;
        /*     */   }
    /*     */
    /*     */   public void setName(String name) {
        /* 126 */     this.name = name;
        /*     */   }
    /*     */
    /*     */   public void setUUID(UUID uuid) {
        /* 130 */     this.uuid = uuid;
        /*     */   }
    /*     */
    /*     */   public void setFeatures(Set<String> features) {
        /* 134 */     if (features != null) {
            /* 135 */       this.features = (Set<String>) ImmutableSet.copyOf(features);
            /*     */     }
        /*     */   }
    /*     */
    /*     */
    /*     */   public boolean equals(Object obj) {
        /* 141 */     if (obj instanceof License) {
            /* 142 */       License license = (License)obj;
            /*     */
            /* 144 */       return (
                    /* 145 */         Objects.equal(getStartDate(), license.getStartDate()) &&
                    /* 146 */         Objects.equal(getExpirationDate(), license.getExpirationDate()) &&
                    /* 147 */         Objects.equal(getDeactivationDate(), license.getDeactivationDate()) &&
                    /* 148 */         Objects.equal(Integer.valueOf(this.version), Integer.valueOf(license.getVersion())) &&
                    /* 149 */         Objects.equal(this.name, license.getName()) &&
                    /* 150 */         Objects.equal(this.uuid, license.getUUID()));
            /*     */     }
        /* 152 */     return super.equals(obj);
        /*     */   }
    /*     */
    /*     */
    /*     */
    /*     */   public int hashCode() {
        /* 158 */     return Objects.hashCode(new Object[] { this.startDate, this.expirationDate, this.deactivationDate, this.name, this.uuid });
        /*     */   }
    /*     */
    /*     */
    /*     */
    /*     */   public String toString() {
        /* 164 */     return MoreObjects.toStringHelper(this)
/* 165 */       .add("Name", this.name)
/* 166 */       .add("UUID", this.uuid)
/* 167 */       .add("Start Date", this.startDate)
/* 168 */       .add("Expiration Date", this.expirationDate)
/* 169 */       .add("Deactivation Date", this.deactivationDate)
/* 170 */       .toString();
        /*     */   }
    /*     */ }
