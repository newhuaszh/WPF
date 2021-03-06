﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;  //add by hand,which is needed when load the layer attribute information
using System.Data.OleDb;
using System.Collections;
using System.Xml;

namespace ShapeFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }

    public class CGShapeFileParser
    {
        public class ESRI_ShxHeader
        {
            int FileCode; //9994
            int[] Unused2 = new int[5];
            int FileLength;
            int Version; //1000
            int ShapeType; // 0- Null shape
            // 1- Point
            // 3-Arc
            // 5-Polygon
            // 8-MultiPoint
            double XMin;
            double YMin;
            double XMax;
            double YMax;
            int[] Unused3 = new int[8];
        }
        class ESRI_ShapeFile
        {
            int FileCode; //9994
            int[] Unused = new int[5];
            int FileLength;
            int Version; //1000
            int ShapeType; // 0- Null shape
            // 1- Point
            // 3-Arc
            // 5-Polygon
            // 8-MultiPoint
            double XMin;
            double YMin;
            double XMax;
            double YMax;
            int[] Unused1 = new int[8];
        }

        class ESRI_RecordHeader
        {
            int RecNumber;
            int ContentLength;
        }
        class ESRI_PointContent
        {
            int ShapeType;
            double X;
            double Y;
        }
        class ESRI_IndexRec//索引文件
        {
            int Offset;
            int ContentLen;
        }
        class ESRI_ArcContent
        {
            int ShapeType;
            double xmin;
            double ymin;
            double xmax;
            double ymax;
            int NumParts;
            int NumPoints;
        }
        class ESRI_PolygonContent
        {
            int ShapeType;
            double xmin;
            double ymin;
            double xmax;
            double ymax;
            int NumParts;
            int NumPoints;
        }

        public bool LoadShapeFile(CGDataAdapter.CGLocalGeoDataAdapter adapter)
        {
            string connectionString;
            OdbcConnection connection;
            OdbcDataAdapter OdbcAdapter;

            CGMap.CGGeoLayer geolayer = adapter.getMasterGeoLayer();
            string shpfilepath = adapter.getPath();
            string shpfilename = adapter.getFileName();
            string shxfilepath = shpfilepath.Substring(0, shpfilepath.LastIndexOf("\\") + 1) + adapter.getFileName() + ".shx";
            //read out the layer attribute infomation
            connectionString = "Dsn=Visual FoxPro Database;sourcedb=" + shpfilepath + ";sourcetype=DBF;exclusive=No;backgroundfetch=Yes;collate=Machine";
            connection = new OdbcConnection(connectionString);
            connection.Open();
            OdbcAdapter = new OdbcDataAdapter("select * from " + shpfilename, connectionString);
            // Create new DataTable and DataSource objects.
            DataSet ds = new DataSet();
            OdbcAdapter.Fill(ds);
            connection.Close();
            if (geolayer == null) return false;
            try
            {
                //先读取.shx文件,得到文件的总字节长度
                FileStream fs = new FileStream(shxfilepath, FileMode.Open, FileAccess.Read);   //文件流形式  
                BinaryReader BinaryFile = new BinaryReader(fs);  //二进制读取文件的对象
                long BytesSum = fs.Length;  //得到文件的字节总长  
                int shapecount = (int)(BytesSum - 100) / 8;  //得以总记录数目            
                BinaryFile.Close();
                fs.Close();
                //打开shp文件
                if (shxfilepath == "")
                {
                    //  MessageBox.Show("索引文件打开出错");
                    return false;
                }
                //打开.shp文件,读取x,y坐标的信息
                fs = new FileStream(shpfilepath, FileMode.Open, FileAccess.Read);   //文件流形式
                BinaryFile = new BinaryReader(fs);     //打开二进制文件  
                BinaryFile.ReadBytes(32);  //先读出36个字节,紧接着是Box边界合
                int shapetype = BinaryFile.ReadInt32();

                geolayer.envlope.left = BinaryFile.ReadDouble();   //读出整个shp图层的边界合
                geolayer.envlope.bottom = BinaryFile.ReadDouble();
                geolayer.envlope.right = BinaryFile.ReadDouble();
                geolayer.envlope.top = BinaryFile.ReadDouble();

                BinaryFile.ReadBytes(32);  //   shp中尚未使用的边界盒   

                //Get Shape Data From Here On
                int stype;
                double x, y;
                double left, right, top, bottom;
                int partcount;
                int pointcount;
                switch (shapetype)
                {
                    case 1://single point
                        geolayer.shapeType = CGConstants.CGShapeType.SHAPE_POINT;
                        for (int i = 0; i < shapecount; i++)
                        {
                            CGGeoShape.CGGeoPoint gps = new CGGeoShape.CGGeoPoint();
                            BinaryFile.ReadBytes(12); //记录头8个字节和一个int(4个字节)的shapetype
                                                      /* stype = BinaryFile.ReadInt32();

                                                       if (stype != shapetype)
                                                           continue;
                                                       */
                            x = BinaryFile.ReadDouble();
                            y = BinaryFile.ReadDouble();
                            gps.objectID = i;
                            gps.objectUID = i;
                            gps.x = x;
                            gps.y = y;
                            gps.z = 0;
                            gps.envlope.left = gps.x;
                            gps.envlope.right = gps.x;
                            gps.envlope.top = gps.y;
                            gps.envlope.bottom = gps.y;
                            geolayer.getDataContainer().Add(gps);
                        }
                        break;
                    case 8://multi points layer
                        break;
                    case 3://Polyline layer
                        geolayer.shapeType = CGConstants.CGShapeType.SHAPE_LINE;
                        for (int i = 0; i < shapecount; i++)
                        {
                            geolayer.getAttributeContainer().Add(ds.Tables[0].Rows[i][0]);      //read out the attribute step by step
                            BinaryFile.ReadBytes(12);
                            //  int pos = indexRecs[i].Offset+8;
                            //  bb0.position(pos);
                            //  stype = bb0.getInt();
                            //  if (stype!=nshapetype){
                            //   continue;
                            //  }
                            left = BinaryFile.ReadDouble();
                            bottom = BinaryFile.ReadDouble();
                            right = BinaryFile.ReadDouble();
                            top = BinaryFile.ReadDouble();
                            partcount = BinaryFile.ReadInt32();
                            pointcount = BinaryFile.ReadInt32();
                            int[] parts = new int[partcount];
                            int[] partspos = new int[partcount];
                            double[] xpoints = new double[pointcount];
                            double[] ypoints = new double[pointcount];
                            double[] zpoints = new double[pointcount];
                            //firstly read out parts begin pos in file
                            for (int j = 0; j < partcount; j++)
                            {
                                parts[j] = BinaryFile.ReadInt32();
                            }
                            //shift them to be points count included in parts
                            if (partcount > 0)
                                partspos[0] = 0;
                            int newpos = 0;
                            for (int j = 0; j <= partcount - 2; j++)
                            {
                                parts[j] = parts[j + 1] - parts[j];
                                newpos += parts[j];
                                partspos[j + 1] = newpos;
                            }
                            parts[partcount - 1] = pointcount - parts[partcount - 1];
                            //read out coordinates
                            for (int j = 0; j < pointcount; j++)
                            {
                                x = BinaryFile.ReadDouble();
                                y = BinaryFile.ReadDouble();
                                xpoints[j] = x;
                                ypoints[j] = y;
                                zpoints[j] = 0;
                            }
                            if (pointcount > 1)
                            {
                                CGGeoShape.CGGeoLine gl = new CGGeoShape.CGGeoLine(xpoints, ypoints, zpoints, parts, partspos, pointcount, partcount);
                                gl.envlope.left = left;
                                gl.envlope.right = right;
                                gl.envlope.top = top;
                                gl.envlope.bottom = bottom;
                                gl.objectID = i;
                                gl.objectUID = i;
                                geolayer.getDataContainer().Add(gl);
                            }
                        }
                        break;
                    case 5://Polygon layer
                        geolayer.shapeType = CGConstants.CGShapeType.SHAPE_POLYGON;
                        for (int i = 0; i < shapecount; i++)
                        {
                            geolayer.getAttributeContainer().Add(ds.Tables[0].Rows[i][0]);
                            /*  bb0.rewind();
                              bb0.position(indexRecs[i].Offset + 8);
                              stype = BinaryFile.ReadInt32();
                              if (stype != shapetype)
                              {
                                  continue;
                              }*/
                            BinaryFile.ReadBytes(12);
                            left = BinaryFile.ReadDouble();
                            bottom = BinaryFile.ReadDouble();
                            right = BinaryFile.ReadDouble();
                            top = BinaryFile.ReadDouble();
                            partcount = BinaryFile.ReadInt32();
                            pointcount = BinaryFile.ReadInt32();
                            int[] parts = new int[partcount];
                            int[] partspos = new int[partcount];
                            double[] xpoints = new double[pointcount];
                            double[] ypoints = new double[pointcount];
                            double[] zpoints = new double[pointcount];
                            //firstly read out parts begin pos in file
                            for (int j = 0; j < partcount; j++)
                            {
                                parts[j] = BinaryFile.ReadInt32();
                            }
                            //shift them to be points count included in parts
                            if (partcount > 0)
                                partspos[0] = 0;
                            int newpos = 0;
                            for (int j = 0; j <= partcount - 2; j++)
                            {
                                parts[j] = parts[j + 1] - parts[j];
                                newpos += parts[j];
                                partspos[j + 1] = newpos;
                            }
                            parts[partcount - 1] = pointcount - parts[partcount - 1];
                            //read out coordinates
                            for (int j = 0; j < pointcount; j++)
                            {
                                x = BinaryFile.ReadDouble();
                                y = BinaryFile.ReadDouble();
                                xpoints[j] = x;
                                ypoints[j] = y;
                                zpoints[j] = 0;
                            }
                            if (pointcount > 1)
                            {
                                CGGeoShape.CGGeoPolygon gl = new CGGeoShape.CGGeoPolygon(xpoints, ypoints, zpoints, parts, partspos, pointcount, partcount);
                                gl.envlope.left = left;
                                gl.envlope.right = right;
                                gl.envlope.top = top;
                                gl.envlope.bottom = bottom;
                                gl.objectID = i;
                                gl.objectUID = i;
                                geolayer.getDataContainer().Add(gl);
                            }
                        }
                        break;
                    default:
                        return false;
                }
            }
            catch (FileNotFoundException e)
            {
                e.ToString();
            }
            /*
                     cgClsLib.cgMapStock.cgGeoLayer geolayer = adapter.getMasterGeoLayer();
                     if (geolayer==null) 
                         return false;
                     try{
                         // build stream connection
                         String fullname = adapter.getPath()+adapter.getFileName();
                         String idxfilename = fullname.Substring(0,fullname.LastIndexOf("\\"))+".shx";
        
                         FileInputStream in00 = new FileInputStream(fullname);
                         ByteBuffer bb0 = ByteBuffer.allocate(in00.available());
                         in00.read(bb0.array());
                         bb0.order(ByteOrder.LITTLE_ENDIAN);
                         in00 = null;
        
                         FileInputStream in10 = new FileInputStream(idxfilename);
                         ByteBuffer bb1 = ByteBuffer.allocate(in10.available());
                         in10.read(bb1.array());
                         bb1.order(ByteOrder.LITTLE_ENDIAN);
                         in10 = null;
        
                         int shapecount = (bb1.capacity()-100)/8;
        
                         //Get Shape Index Information
                         ESRI_IndexRec[] indexRecs = new ESRI_IndexRec[shapecount];
                         bb1.position(100);
                         for( int ii = 0 ; ii< shapecount; ii++)
                         {
                             indexRecs[ii] = new ESRI_IndexRec();
                             indexRecs[ii].Offset = bb1.getInt();
                             indexRecs[ii].ContentLen = bb1.getInt();
                             indexRecs[ii].Offset=((indexRecs[ii].Offset<<24&0xff000000)|(indexRecs[ii].Offset<<8 &0x00ff0000)|(indexRecs[ii].Offset>>8 &0x0000ff00)|(indexRecs[ii].Offset>>24&0x000000ff))<<1;
                         }
        
                         //Get Shape Outline Information
                         bb0.position(32);
                         int nshapetype = bb0.getInt();
                         geolayer.envlope.left = BinaryFile.ReadDouble();();
                         geolayer.envlope.bottom = BinaryFile.ReadDouble();();
                         geolayer.envlope.right = BinaryFile.ReadDouble();();
                         geolayer.envlope.top =BinaryFile.ReadDouble();();
         
                         //Get Shape Data From Here On
                         int stype;
                         double x , y ;
                         double left, right, top, bottom;
        
                         int partcount;
                         int pointcount;
        
                         switch(nshapetype){
                         case 1://single point
             
                             geolayer.shapeType = MKShapeType.SHAPE_POINT;
                             for(int i=0; i<shapecount; i++){
                                 MKGeoPoint gps = new MKGeoPoint();
                                 bb0.position(indexRecs[i].Offset+8);
                                 stype = bb0.getInt();
                                 x = BinaryFile.ReadDouble();();
                                 y = BinaryFile.ReadDouble();();
                                 if (stype!=nshapetype)
                                     continue;
          
                                 gps.objectID = i;
                                 gps.objectUID = i;
                                 gps.x = x;
                                 gps.y = y;
                                 gps.z = 0;
                                 gps.envlope.left = gps.x;
                                 gps.envlope.right = gps.x;
                                 gps.envlope.top = gps.y;
                                 gps.envlope.bottom = gps.y;
                                 geolayer.getDataContainer().add(gps);
                             }
                             break;
         
                         case 8://multi points layer
                             break;
         
                         case 3://Polyline layer
                             geolayer.shapeType = MKShapeType.SHAPE_LINE;
         
                             for(int i=0; i<shapecount; i++){
                                 int pos = indexRecs[i].Offset+8;
                                 bb0.position(pos);
                                 stype = bb0.getInt();
                                 if (stype!=nshapetype){
                                     continue;
                                 }
                                 left = BinaryFile.ReadDouble();();
                                 bottom = BinaryFile.ReadDouble();();
                                 right =  BinaryFile.ReadDouble();();
                                 top =  BinaryFile.ReadDouble();();
                                 partcount =  bb0.getInt();
                                 pointcount = bb0.getInt();
                                 int[] parts = new int[partcount];
                                 int[] partspos = new int[partcount];
              
                                 double[] xpoints = new double[pointcount];
                                 double[] ypoints = new double[pointcount];
                                 double[] zpoints = new double[pointcount];
          
                                 //firstly read out parts begin pos in file
                                 for(int j = 0; j < partcount;j++){
                                     parts[j]= bb0.getInt();
                                 }
                                 //shift them to be points count included in parts
                                 if (partcount > 0)
                                     partspos[0]=0;
          
                                 int newpos=0;
                                 for(int j = 0; j <= partcount-2;j++){
                                     parts[j]= parts[j+1] - parts[j];
                                     newpos+=parts[j];
                                     partspos[j+1]=newpos;
                                 }
          
                                 parts[partcount-1]=pointcount-parts[partcount-1];
                                 //read out coordinates
                                 for(int j = 0 ; j < pointcount;j++){
                                     x= BinaryFile.ReadDouble();();
                                     y= BinaryFile.ReadDouble();();
                                     xpoints[j] = x;
                                     ypoints[j] = y;
                                     zpoints[j] = 0;
                                 }
                                 if (pointcount > 1){
                                     cgClsLib.cgGeoObject.cgGeoLine gl = new cgClsLib.cgGeoObject.cgGeoLine(xpoints,ypoints,zpoints,parts,partspos,pointcount,partcount);
                                     gl.envlope.left = left;
                                     gl.envlope.right = right;
                                     gl.envlope.top = top;
                                     gl.envlope.bottom = bottom;                         
           
                                     gl.objectID = i;
                                     gl.objectUID = i;
                                     geolayer.getDataContainer().add(gl);
                                 }
                             }     
                             break;
                         case 5://Polygon layer
                             geolayer.shapeType = MKShapeType.SHAPE_POLYGON;
                             for(int i=0; i<shapecount; i++){
                                 bb0.rewind();
                                 bb0.position(indexRecs[i].Offset+8);
                                 stype =  bb0.getInt();
      
                                 if (stype!=nshapetype){
                                     continue;
                                 }
                                 left =  BinaryFile.ReadDouble();();
                                 bottom =  BinaryFile.ReadDouble();();
                                 right =  BinaryFile.ReadDouble();();
                                 top =  BinaryFile.ReadDouble();();
                                 partcount =  bb0.getInt();
                                 pointcount = bb0.getInt();
                                 int[] parts = new int[partcount];
                                 int[] partspos = new int[partcount];
              
                                 double[] xpoints = new double[pointcount];
                                 double[] ypoints = new double[pointcount];
                                 double[] zpoints = new double[pointcount];
          
                                 //firstly read out parts begin pos in file
                                 for(int j = 0; j < partcount;j++){
                                     parts[j]= bb0.getInt();
                                 }
                                 //shift them to be points count included in parts
                                 if (partcount > 0)
                                     partspos[0]=0;
         
                                 int newpos=0;
                                 for(int j = 0; j <= partcount-2;j++){
                                     parts[j]= parts[j+1] - parts[j];
                                     newpos+=parts[j];
                                     partspos[j+1]=newpos;
                                 }
          
                                 parts[partcount-1]=pointcount-parts[partcount-1];
                                 //read out coordinates
                                 for(int j = 0 ; j < pointcount;j++){
                                     x= BinaryFile.ReadDouble();();
                                     y= BinaryFile.ReadDouble();();
                                     xpoints[j] = x;
                                     ypoints[j] = y;
                                     zpoints[j] = 0;
                                 }
                                 if (pointcount>1){
                                     MKGeoPolygon gl = new MKGeoPolygon(xpoints,ypoints,zpoints,parts,partspos,pointcount,partcount);
                                     gl.envlope.left = left;
                                     gl.envlope.right = right;
                                     gl.envlope.top = top;
                                     gl.envlope.bottom = bottom;
                                     gl.objectID = i;
                                     gl.objectUID = i;
                                     geolayer.getDataContainer().add(gl);
                                 }
                             }      
                             break;
                         default :
                       
                            // System.out.println(NotifyMessages.NO_SUCHTYPE);
                             return false;
                         }
                     }
                     catch(FileNotFoundException e){
                        return false;
                     }
                     catch(EOFException e2){
                         return false;
                     }
                     catch(IOException e1){
                         return false;
                     }
                     catch(Exception e){
                         return false;
                     }*/
            return true;
        }
        public bool SaveShapeFile(CGDataAdapter.CGLocalGeoDataAdapter adapter)
        {
            return true;
        }
    }
}
