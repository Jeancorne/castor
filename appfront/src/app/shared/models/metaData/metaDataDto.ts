import { InfoMetaData } from "./infoMetaData";

export class MetaDataDto<T> extends InfoMetaData {
    data: T[] = [];
}