import { InfoMetaData } from "./infoMetaData";

export class MetaDataDtoObject<T> extends InfoMetaData {
    data!: T;
}