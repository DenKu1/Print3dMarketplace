import { SubmittedCreatorModel } from "./submittedCreatorModel";

export interface PrintRequestModel {
  id: string;
  applicationUserId: string;

  printRequestStatusName: string;

  submittedCreators: SubmittedCreatorModel[];
  customerSubmittedCreatorId: string;

  templateMaterialId: string;
  colorId: string;

  layerHeight: number;
  infill: number;

  printAreaLength: number;
  printAreaWidth: number;
  printAreaHeight: number;
  formFile: File;

  comment: string | null;
  useSupports: boolean | null;
  wallThickness: number | null;

  isActive: boolean;
}
