export interface PrintRequestModel {
  id: string;

  printRequestStatusName: string;

  applicationUserId: string;
  templateMaterialId: string;
  nozzleId: string;
  colorId: string;
  modelId: string;

  infill: number;

  printAreaLength: number;
  printAreaWidth: number;
  printAreaHeight: number;

  borderWidth: number;
  layerHeight: number;

  comment: string;

  useSupports: boolean;
  isActive: boolean;
}
