import { PhotoComponent } from "../../shared/photo/photo.component";

export interface IPhotoService {
    getAllPhotos(subFolder: string) : Promise<string[]>
    getApprovedRoutes() : Promise<string[]>
}