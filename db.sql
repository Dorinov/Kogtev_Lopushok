PGDMP         :                {         	   demo30.01    15.1    15.1 &    1           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            2           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            3           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            4           1262    81920 	   demo30.01    DATABASE        CREATE DATABASE "demo30.01" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "demo30.01";
                postgres    false            �            1259    98426    Account    TABLE     �   CREATE TABLE public."Account" (
    "ID" integer NOT NULL,
    "Login" text NOT NULL,
    "Password" text NOT NULL,
    "Rules" integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."Account";
       public         heap    postgres    false            �            1259    98432    Account_ID_seq    SEQUENCE     �   ALTER TABLE public."Account" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Account_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    214            �            1259    98433    Material    TABLE     �   CREATE TABLE public."Material" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL,
    "Description" text,
    "MaterialTypeID" integer NOT NULL
);
    DROP TABLE public."Material";
       public         heap    postgres    false            �            1259    98438    MaterialType    TABLE     ]   CREATE TABLE public."MaterialType" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL
);
 "   DROP TABLE public."MaterialType";
       public         heap    postgres    false            �            1259    98443    MaterialType_ID_seq    SEQUENCE     �   ALTER TABLE public."MaterialType" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."MaterialType_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            �            1259    98444    Material_ID_seq    SEQUENCE     �   ALTER TABLE public."Material" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Material_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216            �            1259    98445    Product    TABLE     �   CREATE TABLE public."Product" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL,
    "ProductTypeID" integer,
    "ArticleNumber" text NOT NULL,
    "Description" text,
    "Image" text,
    "Cost" double precision NOT NULL
);
    DROP TABLE public."Product";
       public         heap    postgres    false            �            1259    98450    ProductMaterial    TABLE     �   CREATE TABLE public."ProductMaterial" (
    "ProductID" integer NOT NULL,
    "MaterialID" integer NOT NULL,
    "Count" integer
);
 %   DROP TABLE public."ProductMaterial";
       public         heap    postgres    false            �            1259    98453    ProductType    TABLE     \   CREATE TABLE public."ProductType" (
    "ID" integer NOT NULL,
    "Title" text NOT NULL
);
 !   DROP TABLE public."ProductType";
       public         heap    postgres    false            �            1259    98458    ProductType_ID_seq    SEQUENCE     �   ALTER TABLE public."ProductType" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."ProductType_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �            1259    98459    Product_ID_seq    SEQUENCE     �   ALTER TABLE public."Product" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Product_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            �            1259    98460    Storage    TABLE     c   CREATE TABLE public."Storage" (
    "ID" integer NOT NULL,
    "Title" text,
    "Address" text
);
    DROP TABLE public."Storage";
       public         heap    postgres    false            �            1259    98465    StorageAvailability    TABLE     u   CREATE TABLE public."StorageAvailability" (
    "StorageID" integer,
    "ProductID" integer,
    "Count" integer
);
 )   DROP TABLE public."StorageAvailability";
       public         heap    postgres    false            �            1259    98468    Storage_ID_seq    SEQUENCE     �   ALTER TABLE public."Storage" ALTER COLUMN "ID" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Storage_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    225            !          0    98426    Account 
   TABLE DATA           G   COPY public."Account" ("ID", "Login", "Password", "Rules") FROM stdin;
    public          postgres    false    214   g(       #          0    98433    Material 
   TABLE DATA           T   COPY public."Material" ("ID", "Title", "Description", "MaterialTypeID") FROM stdin;
    public          postgres    false    216   �(       $          0    98438    MaterialType 
   TABLE DATA           7   COPY public."MaterialType" ("ID", "Title") FROM stdin;
    public          postgres    false    217   �*       '          0    98445    Product 
   TABLE DATA           t   COPY public."Product" ("ID", "Title", "ProductTypeID", "ArticleNumber", "Description", "Image", "Cost") FROM stdin;
    public          postgres    false    220   +       (          0    98450    ProductMaterial 
   TABLE DATA           O   COPY public."ProductMaterial" ("ProductID", "MaterialID", "Count") FROM stdin;
    public          postgres    false    221   L1       )          0    98453    ProductType 
   TABLE DATA           6   COPY public."ProductType" ("ID", "Title") FROM stdin;
    public          postgres    false    222   �2       ,          0    98460    Storage 
   TABLE DATA           =   COPY public."Storage" ("ID", "Title", "Address") FROM stdin;
    public          postgres    false    225   @3       -          0    98465    StorageAvailability 
   TABLE DATA           R   COPY public."StorageAvailability" ("StorageID", "ProductID", "Count") FROM stdin;
    public          postgres    false    226   �3       5           0    0    Account_ID_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public."Account_ID_seq"', 1, true);
          public          postgres    false    215            6           0    0    MaterialType_ID_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."MaterialType_ID_seq"', 5, true);
          public          postgres    false    218            7           0    0    Material_ID_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Material_ID_seq"', 50, true);
          public          postgres    false    219            8           0    0    ProductType_ID_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public."ProductType_ID_seq"', 10, true);
          public          postgres    false    223            9           0    0    Product_ID_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Product_ID_seq"', 116, true);
          public          postgres    false    224            :           0    0    Storage_ID_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public."Storage_ID_seq"', 3, true);
          public          postgres    false    227            �           2606    98470    Account Account_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY ("ID");
 B   ALTER TABLE ONLY public."Account" DROP CONSTRAINT "Account_pkey";
       public            postgres    false    214            �           2606    98472    MaterialType MaterialType_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."MaterialType"
    ADD CONSTRAINT "MaterialType_pkey" PRIMARY KEY ("ID");
 L   ALTER TABLE ONLY public."MaterialType" DROP CONSTRAINT "MaterialType_pkey";
       public            postgres    false    217            �           2606    98474    Material Material_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Material"
    ADD CONSTRAINT "Material_pkey" PRIMARY KEY ("ID");
 D   ALTER TABLE ONLY public."Material" DROP CONSTRAINT "Material_pkey";
       public            postgres    false    216            �           2606    98476    ProductType ProductType_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."ProductType"
    ADD CONSTRAINT "ProductType_pkey" PRIMARY KEY ("ID");
 J   ALTER TABLE ONLY public."ProductType" DROP CONSTRAINT "ProductType_pkey";
       public            postgres    false    222            �           2606    98478    Product Product_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT "Product_pkey" PRIMARY KEY ("ID");
 B   ALTER TABLE ONLY public."Product" DROP CONSTRAINT "Product_pkey";
       public            postgres    false    220            �           2606    98480    Storage Storage_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Storage"
    ADD CONSTRAINT "Storage_pkey" PRIMARY KEY ("ID");
 B   ALTER TABLE ONLY public."Storage" DROP CONSTRAINT "Storage_pkey";
       public            postgres    false    225            !   $   x�3�4�452400765�44261�44������ G��      #   
  x��V�q�0=[U�����4�
(��Ifr�C
H�$�@BLZXu��-���G�`io߾����t����-^��X`�w��/��ex���nП�od���$���G<�m�~�~�)�#��<7��F�� ,fF7���\�^$S��Y���.�/�z�00��V,	Je!�"$K�ewo��E���üJ��
����l���O	/��	[:��'�:'?����X+߆4�	�S4ׅo5�,`�ҫk-�vL��A �Q�������H&���6��t)M��DO��̕�]z�[���M�A@S��<0�	�"Tw����#Cg���@7�����E���ԛ4�b�V���ۦ,橫!�ѝ�I#r.r�n�9I7��a�0��W'ѦFp��@\m�)��[كd�&�z�i�r;$��$'F��͟+�����jg���ʽ�z�3���g8���1����6A�¾ɴM�q�+mw�f3D��L稣�����IP�ym�� Jf ���韴�[�1��]:�(      $   G   x�3�0�bÅ�/6rs^Xp�����.��2�0(���^���n.S�s/l )���®�b���� Ӆ$f      '   0  x�uX[�]E�>=�;���ٽ&��G!���?
�K�Q������s���υ��ףVU�.������vy���������)˯�/���o�?�w�?˿˛�8X�ޞ��I8%�����oy����y`_#�k���n~SU��G�\qe�����j�=�\�m�����z`[c^?�B���ME	����$+����k�ݤD�y�*��f�PZ�~~����8��Ss���ۛbR��h=�3GT��'7���[D傀��G�����㶜���6ծ��Tm�J�ӵ��.�M*g��R$�{u&_���x�����y�M�r�QN\����^es��5J��c,�	���$�9I�����"1ʾ�O�r����]��y�p�S皤���M��C,�i�HbMI�U��PpI�AX�_��~=[�2�t<g��m���+�[l�3�NA�iF���HQ';o@9ڡ]��BX㣜��)��E)����B����8��L��$�R=i�l��>� ���f"I��vΘ1��C0<Mj]׹g ��()��K�+9���V�'i���	iA0�}��P��VSG��џ�Oȹ�J�]Q͡�R�՜�Yf�淊]�zd�6[Gޥ� �bK����B��7O��M��d��O�����@,�6:���d���J��C�O�z'��P��f�$oV��ˬ ��֣o�u܈�����̐ �M%��qX��:����q~��@&����ɧ���/	�K��Y'�T��n�5��;xs@�qv���ӽ���������a���9E�g��Yٴq��b����Ϯ,ݕR����!y�씑�	Z������������p����E�+`sWA�9թ���J:�d�jW�6�^=,�)����i/K���`K0��3d����Cc��jg�q�����Q��2��5;�@>��jrm��S���S�C��̚j�Ȱ�O?Ȱ�EF�&-�ȅ�D���qǮ1��76Yp@��M�p�d�����1�lkh����=v�R��������d  ��/˽J9��ht��E�:!�>��Ƌq���7�� �����~���^~���W�p~�����In�Q�:�.���+"�	7:���-�=�طn^�x���[�&����Y&�B\�&���S�L�.��j%�	�fˆ�w��v��]���{�8�.΂��/Dsݏr`*��S˛�Nw����O�3p���+ְhca��Y��K�|����SE��V�0��T'��"�x��_}���P��F0�Z�Ö�Mz���<��[�S6)�,%*[c��qɔe�W`����[n�ߕ�L��I��.�/UsB�s)x'[�\��)������)ۨ�SM6�X��o}��xo�Ã�{ny�4��lG(�M���{td<�xձ=�A�M�R�e��<�ž�s�{w�"����/^jM�fj1��4���r�S�q/'ߌ*)����Y��~��;�n<��@1㘩DPo�rd���9?K����W�P��۹M+�*sk>8��N���6�@�Q��M?~����1M������L%fV�jh���!�ƛ
�e��;��Ol:�Qz�,��?��Y      (   y  x�5�Q�� D��0�
�z���9v�&MPaf��s�c{��b+��Cz��}�d{͇ǘ�J��9��6/	�>]r�
����غ ��~�ձl?��_��C�� �֢F�|�Bq0m��~���yM�}�E��y�8�fQB��Y�#����HZ��@����4������#��x�2�7i-:�ɽ�*]�:b�d��yO.nc�."�X�/��(�+���w!/����n3� �M�����9ZD-�m9,�����GU\����xzb��S>��|4��5T�%P�N�Tz�� �7����,�x���_���et�e�,N)u�d$��&�!�,�B	��]S��2�7�3֠�EA��Ƈ����Q������� a\v�      )   [   x�Eʹ	�@ �xV`�U��x`b,h������\a���D���h���&��eb���$�A�/���Z9�d�^k�E���/y("/Ĳ<�      ,   �   x�3估�®�/l��E�Q�4C���.L���bÅ}�^�za��������������2{.6�m�د�pa������QF��Z���$�Ƙ��r�d�Ϡ	6�]2b�1W� �}�      -   �  x�5�Q�-+C�{敂 ���O�:_)i�������7?��������B���*u�g� Ϧ����j�뫇�:��#f��-���M����cK*,��8�ϊ���.��M}���8^>ǭ�������Mk�.�.��d�|��߀/����Q�/��-�(��9��}i�O�%|�7[�&��y$|}�yl�y){��};�߬����
}_1�Zꯠ�:���૖���T��o㷙G�����9ݼ���{s8ý�w��v��m>����R�W�ַ9�C���x���	����^�k�� ��a�� ��-^�n ^���S��d�q��yП���K:����cM�-c��]�U �B��U���=�x�� ���;�x�*v�}��}	_�/�;�x��߄/�G�w�����ߞ�����ѷ����m�n�6�(�[̷L�}����WI��o᷇����k��y4��k����@��)���z�z���"�LO�����K�=�����X?A~����7�:��3S�9�ސ���@�B����s�w�O��a��7��������2�-���w������U����Ŕ��o���o�~�i��%~�d�C8���Ǜ��r�.f�گ@8���׿��ۡs�����6��[�_��xy�{)����R�-��C�z��W�������e���.�y��_���݀�����~����VH�     