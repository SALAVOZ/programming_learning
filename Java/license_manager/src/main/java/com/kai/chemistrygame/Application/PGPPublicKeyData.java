package com.kai.chemistrygame.Application;


import com.google.common.base.Splitter;
import com.google.gson.Gson;
import org.bouncycastle.openpgp.PGPPublicKey;

import javax.xml.bind.DatatypeConverter;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
public class PGPPublicKeyData {
    String keyID;
    String userID;
    String firstName;
    String lastName;
    String userEmail;
    Date created;
    Date exp;
    int bitStrength;
    String asciiArmored;
    String fingerprint;

    public PGPPublicKeyData(PGPPublicKey pgpPublicKey) {
        // keyID
        StringBuilder keyIDstrBuilder = new StringBuilder();
        keyIDstrBuilder.append("[0x");
        keyIDstrBuilder.append(Integer.toHexString((int) pgpPublicKey.getKeyID()).toUpperCase());
        keyIDstrBuilder.append("] ");
        this.keyID = keyIDstrBuilder.toString();
        // userID
        StringBuilder userIDstrBuilder = new StringBuilder();
        Iterator userIDsIterator = pgpPublicKey.getUserIDs();
        while (userIDsIterator.hasNext()) {
            userIDstrBuilder.append(userIDsIterator.next().toString());
            userIDstrBuilder.append("; ");
        }
        this.userID = userIDstrBuilder.toString();
        // user's first and last name and email
        List<String> userIdList = Splitter.on(' ').trimResults().omitEmptyStrings().splitToList(userID);
        this.firstName = userIdList.get(0);
        this.lastName = userIdList.get(1);
        String userEmailDirty = userIdList.get(userIdList.size() - 1);
        this.userEmail = userEmailDirty.substring(
                1, userEmailDirty.length() - 2
        );
        // fingerprint // [0xE77173E5]
        this.fingerprint = DatatypeConverter.printHexBinary(
                pgpPublicKey.getFingerprint()
        );
        // creation date
        this.created = pgpPublicKey.getCreationTime();
        // exp date
        this.exp = org.apache.commons.lang3.time.DateUtils.addSeconds(created, (int) pgpPublicKey.getValidSeconds());
        // Bit strength
        this.bitStrength = pgpPublicKey.getBitStrength();
    }

    public String toJSON() {
        Gson gson = new Gson();
        return gson.toJson(this);
    }

}
