PGDMP         !                {         	   demo30.01    15.1    15.1     #           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            $           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            %           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            &           1262    81920 	   demo30.01    DATABASE        CREATE DATABASE "demo30.01" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "demo30.01";
                postgres    false            �            1259    98407    Account    TABLE     �   CREATE TABLE public."Account" (
    "ID" integer NOT NULL,
    "Login" text NOT NULL,
    "Password" text NOT NULL,
    "Rules" integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."Account";
       public         heap    postgres    false            �            1259    98406    Account_ID_seq    SEQUENCE     �   ALTER TABLE public."Account" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Account_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    224            �            1259    81950    Material    TABLE     �   CREATE TABLE public."Material" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL,
    "Description" text,
    "MaterialTypeID" integer NOT NULL
);
    DROP TABLE public."Material";
       public         heap    postgres    false            �            1259    81967    MaterialType    TABLE     ]   CREATE TABLE public."MaterialType" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL
);
 "   DROP TABLE public."MaterialType";
       public         heap    postgres    false            �            1259    81966    MaterialType_ID_seq    SEQUENCE     �   ALTER TABLE public."MaterialType" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."MaterialType_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            �            1259    81949    Material_ID_seq    SEQUENCE     �   ALTER TABLE public."Material" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Material_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    215            �            1259    81975    Product    TABLE     �   CREATE TABLE public."Product" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL,
    "ProductTypeID" integer,
    "ArticleNumber" text NOT NULL,
    "Description" text,
    "Image" text,
    "Cost" double precision NOT NULL
);
    DROP TABLE public."Product";
       public         heap    postgres    false            �            1259    81988    ProductMaterial    TABLE     �   CREATE TABLE public."ProductMaterial" (
    "ProductID" integer NOT NULL,
    "MaterialID" integer NOT NULL,
    "Count" integer
);
 %   DROP TABLE public."ProductMaterial";
       public         heap    postgres    false            �            1259    81998    ProductType    TABLE     \   CREATE TABLE public."ProductType" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL
);
 !   DROP TABLE public."ProductType";
       public         heap    postgres    false            �            1259    81997    ProductType_ID_seq    SEQUENCE     �   ALTER TABLE public."ProductType" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."ProductType_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �            1259    81974    Product_ID_seq    SEQUENCE     �   ALTER TABLE public."Product" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Product_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    219                       0    98407    Account 
   TABLE DATA           G   COPY public."Account" ("ID", "Login", "Password", "Rules") FROM stdin;
    public          postgres    false    224   !                 0    81950    Material 
   TABLE DATA           T   COPY public."Material" ("ID", "Title", "Description", "MaterialTypeID") FROM stdin;
    public          postgres    false    215   P!                 0    81967    MaterialType 
   TABLE DATA           7   COPY public."MaterialType" ("ID", "Title") FROM stdin;
    public          postgres    false    217   j#                 0    81975    Product 
   TABLE DATA           t   COPY public."Product" ("ID", "Title", "ProductTypeID", "ArticleNumber", "Description", "Image", "Cost") FROM stdin;
    public          postgres    false    219   �#                 0    81988    ProductMaterial 
   TABLE DATA           O   COPY public."ProductMaterial" ("ProductID", "MaterialID", "Count") FROM stdin;
    public          postgres    false    220   4*                 0    81998    ProductType 
   TABLE DATA           6   COPY public."ProductType" ("ID", "Title") FROM stdin;
    public          postgres    false    222   �+       '           0    0    Account_ID_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public."Account_ID_seq"', 1, true);
          public          postgres    false    223            (           0    0    MaterialType_ID_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."MaterialType_ID_seq"', 5, true);
          public          postgres    false    216            )           0    0    Material_ID_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Material_ID_seq"', 50, true);
          public          postgres    false    214            *           0    0    ProductType_ID_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."ProductType_ID_seq"', 6, true);
          public          postgres    false    221            +           0    0    Product_ID_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Product_ID_seq"', 109, true);
          public          postgres    false    218            �           2606    98411    Account Account_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY ("ID");
 B   ALTER TABLE ONLY public."Account" DROP CONSTRAINT "Account_pkey";
       public            postgres    false    224            �           2606    81971    MaterialType MaterialType_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."MaterialType"
    ADD CONSTRAINT "MaterialType_pkey" PRIMARY KEY ("ID");
 L   ALTER TABLE ONLY public."MaterialType" DROP CONSTRAINT "MaterialType_pkey";
       public            postgres    false    217                       2606    81954    Material Material_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Material"
    ADD CONSTRAINT "Material_pkey" PRIMARY KEY ("ID");
 D   ALTER TABLE ONLY public."Material" DROP CONSTRAINT "Material_pkey";
       public            postgres    false    215            �           2606    82002    ProductType ProductType_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."ProductType"
    ADD CONSTRAINT "ProductType_pkey" PRIMARY KEY ("ID");
 J   ALTER TABLE ONLY public."ProductType" DROP CONSTRAINT "ProductType_pkey";
       public            postgres    false    222            �           2606    81979    Product Product_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT "Product_pkey" PRIMARY KEY ("ID");
 B   ALTER TABLE ONLY public."Product" DROP CONSTRAINT "Product_pkey";
       public            postgres    false    219                $   x�3�4�452400765�44261�44������ G��         
  x��V�q�0=[U�����4�
(��Ifr�C
H�$�@BLZXu��-���G�`io߾����t����-^��X`�w��/��ex���nП�od���$���G<�m�~�~�)�#��<7��F�� ,fF7���\�^$S��Y���.�/�z�00��V,	Je!�"$K�ewo��E���üJ��
����l���O	/��	[:��'�:'?����X+߆4�	�S4ׅo5�,`�ҫk-�vL��A �Q�������H&���6��t)M��DO��̕�]z�[���M�A@S��<0�	�"Tw����#Cg���@7�����E���ԛ4�b�V���ۦ,橫!�ѝ�I#r.r�n�9I7��a�0��W'ѦFp��@\m�)��[كd�&�z�i�r;$��$'F��͟+�����jg���ʽ�z�3���g8���1����6A�¾ɴM�q�+mw�f3D��L稣�����IP�ym�� Jf ���韴�[�1��]:�(         G   x�3�0�bÅ�/6rs^Xp�����.��2�0(���^���n.S�s/l )���®�b���� Ӆ$f         c  x�uX[�%E�>������Y��p��1
 ��Q��|�W0"��k�#��t��z\�'��2##��\�_���7���������)�O�˛���˿�_��˫_8X�^�{w�'ᔈ.�w��?˫�����=�Z��\���EUE��sI�m��>�.j�%�(rI$��g��w�������3D���T�����I��m�>��EJԜ��l��5���+���XBt/�Yj"oO��}�E�Q�9�JW߹<��g�"*Ĭ<���-����8���vd�j��T)1�Nq�_�r���,EϻWg`�LH��c8���^�^�*�~�"ʉ��\���l���F�pp��ÄK	�a�$���x�e��j9I��ۮ�żW8sM��	��$A����s$��J�Q��PpI�AX�_��~=[�2�t<g��i�������bg��2�@ҌHs���Nv?����-���)���hz)�t9�[(�����Þ��܉�TO�0��� �,wv3��Sv�Ϙ1��C0<Mj]׹g ���R.�LWr ��5��2�O�2m�#҂>` �k��ӭ�������>��*�vE5��K�Vs�f�iZ�*btV�5�ly�
�<X�-Y;d��
=��X<�g4Y�_�Ş�8+.ҏW�G ����X�R�y�G:�:�/�}'L�w��E�ެ`���j�v� ��z�-��1�T"ڙ�����!�tRG����2ΏX ���^5�T�Y�%�tI1�ߓJ*�j����c��9 ��8;�YΉ��{P\���i��0�Ul����KͬlڸUE���b�gG��H�^w��Fv����`OT��z.�[�r�r�N��T��t����� ���TO�h%
E2.�jw=mf�z X��R��,kX��-� �/�P���5(z��e��Q�^�� �R��Q��Ձ���U�k3,�:-��2�e�TcF�-�A�.2:�0iG.w4i
w��o}�a�	RLl%�:{�+z�ɶ�f���`�(EOi���^� �Bs��e�W)]�X��qBw�O�Ca��b̭�֑&��\���*��e�ƫ ח?����O�y��󧟼���Gϟ>�Q��,��[{�oܼ$��fc��Mα�ɳLnq�~�l��O�3a:�N���&Ě-f��ތk����
���`�x��u[�&�R90����Mq���jnnܭ���Ċ5,�x1�#�+��,_�� �|�T�V�]H�h��sm��o��&�m
��&Q+w��IO}�o�'�zkz�&嘕DekLv8.����
�1�x���R���/��nK�d�{-�dk���5e��0�uȲ��8�d�%�ַ)��x���[�pˣ ��	�dK�(6A&�ё��U��l��C5�K��Q���R���}�Q�ъ��S�$[_�Ԛ
��b�<iȵ�r�S�q/'ߌ*)����U��~��;�n�B|���f*���'��s��Ri��%�� ���vn�
ư�ܚ���S}2�?Pl�l�����j��&�A����L%fV�lh��I�m�M�2b��V�'���VW�*^.��/���X�~���??��xX�]~�� ,� Ӟ�����"���0&�ޓ���t���         �  x�5�[\1C��b���^��uԇL���l��9b������-�´u)����Ns>�bD_�ukQ��5<̋��?C��4�".�k� ������|�K���IS,�F���6n~�:���*�f�Ƈ�ŀ�Y���|�B��Y�s��LAl-�������FJ���#���>�e8��3ZtzRD�Uy?u�fI��}'����H�6�''H6����w!/�������
p��h��f�Q�ٶ\�e)�������_o�^��#|��lƵ����}���
A���  �ٽׂ�bQ�ǻ�����~_A'GF��&[S�%���~�l��E��-������5�%ِg���B���Fl�Ϋşb��XE|������z         [   x�Eʹ	�@ �xV`�U��x`b,h������\a���D���h���&��eb���$�A�/���Z9�d�^k�E���/y("/Ĳ<�     